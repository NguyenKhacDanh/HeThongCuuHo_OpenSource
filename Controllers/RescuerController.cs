using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RescueHub.Data;
using RescueHub.Models.Entities;
using RescueHub.Models.Enums;
using RescueHub.Models.Identity;
using RescueHub.Services;

namespace RescueHub.Controllers
{
    [Authorize(Roles = "Rescuer")]
    public class RescuerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGeoService _geo;
        private readonly INotificationService _notify;

        public RescuerController(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            IGeoService geo,
            INotificationService notify)
        {
            _db = db;
            _userManager = userManager;
            _geo = geo;
            _notify = notify;
        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            var profile = await _db.RescuerProfiles.FindAsync(user!.Id);

            var query = _db.UserRequests
                .Where(x => x.Status == RequestStatus.New ||
                            x.AssignedRescuerId == user.Id)
                .OrderByDescending(x => x.RiskLevel)
                .ThenBy(x => x.CreatedAt);

            var requests = await query.Take(50).ToListAsync();

            var items = requests.Select(r => new RescuerDashboardItem
            {
                Request = r,
                DistanceKm = _geo.DistanceKm(profile?.AreaLatitude, profile?.AreaLongitude, r.Latitude, r.Longitude)
            }).ToList();

            return View(items);
        }

        public async Task<IActionResult> RequestDetails(int id)
        {
            var request = await _db.UserRequests.FindAsync(id);
            if (request == null) return NotFound();
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Assign(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var req = await _db.UserRequests.FindAsync(id);
            if (req == null) return NotFound();

            if (req.Status == RequestStatus.New || req.AssignedRescuerId == null)
            {
                req.AssignedRescuerId = user!.Id;
                req.Status = RequestStatus.Assigned;
                req.UpdatedAt = DateTime.UtcNow;

                await _db.SaveChangesAsync();

                await _notify.NotifyUserAsync(req,
                    $"Yêu cầu {req.RequestCode} đã được đội cứu hộ tiếp nhận.");
            }

            return RedirectToAction("RequestDetails", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> StartRescue(int id)
        {
            var req = await _db.UserRequests.FindAsync(id);
            if (req == null) return NotFound();

            req.Status = RequestStatus.InProgress;
            req.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();

            return RedirectToAction("RequestDetails", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Complete(int id, string? notes)
        {
            var req = await _db.UserRequests.FindAsync(id);
            if (req == null) return NotFound();

            req.Status = RequestStatus.Completed;
            req.NotesFromRescuer = notes;
            req.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();

            await _notify.NotifyUserAsync(req,
                $"Yêu cầu {req.RequestCode} đã được đánh dấu hoàn thành.");

            return RedirectToAction("RequestDetails", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> CannotReach(int id, string? notes)
        {
            var req = await _db.UserRequests.FindAsync(id);
            if (req == null) return NotFound();

            req.Status = RequestStatus.CannotReach;
            req.NotesFromRescuer = notes;
            req.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();

            await _notify.NotifyUserAsync(req,
                $"Đội cứu hộ tạm thời không thể tiếp cận yêu cầu {req.RequestCode}. Hãy giữ an toàn.");

            return RedirectToAction("RequestDetails", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> RequestsJson()
        {
            var data = await _db.UserRequests
                .Where(x => x.Status == RequestStatus.New ||
                            x.Status == RequestStatus.Assigned ||
                            x.Status == RequestStatus.InProgress)
                .Select(x => new
                {
                    id = x.Id,
                    code = x.RequestCode,
                    name = x.FullName,
                    phone = x.PhoneNumber,
                    risk = x.RiskLevel,
                    status = x.Status,
                    lat = x.Latitude,
                    lng = x.Longitude,
                    createdAt = x.CreatedAt
                }).ToListAsync();

            return Json(data);
        }

        public IActionResult Map()
        {
            return View();
        }
    }

    public class RescuerDashboardItem
    {
        public UserRequest Request { get; set; } = default!;
        public double? DistanceKm { get; set; }
    }
}
