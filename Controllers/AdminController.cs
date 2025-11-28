using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RescueHub.Data;
using RescueHub.Models.Enums;
using System.Text;

namespace RescueHub.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Dashboard()
        {
            var totalByStatus = await _db.UserRequests
                .GroupBy(x => x.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            var now = DateTime.UtcNow;
            var newLastHour = await _db.UserRequests
                .CountAsync(x => x.CreatedAt >= now.AddHours(-1));
            var newLast24h = await _db.UserRequests
                .CountAsync(x => x.CreatedAt >= now.AddHours(-24));

            ViewBag.TotalByStatus = totalByStatus;
            ViewBag.NewLastHour = newLastHour;
            ViewBag.NewLast24h = newLast24h;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> MapDataJson()
        {
            var reqs = await _db.UserRequests
                .Select(x => new
                {
                    type = "request",
                    id = x.Id,
                    name = x.FullName,
                    code = x.RequestCode,
                    lat = x.Latitude,
                    lng = x.Longitude,
                    risk = x.RiskLevel,
                    status = x.Status
                }).ToListAsync();

            var safes = await _db.SafeReports
                .Select(x => new
                {
                    type = "safe",
                    id = x.Id,
                    name = x.FullName,
                    lat = x.Latitude,
                    lng = x.Longitude
                }).ToListAsync();

            var shelters = await _db.Shelters
                .Where(s => s.IsActive)
                .Select(x => new
                {
                    type = "shelter",
                    id = x.Id,
                    name = x.Name,
                    lat = x.Latitude,
                    lng = x.Longitude,
                    capacity = x.Capacity,
                    current = x.CurrentPeople
                }).ToListAsync();

            return Json(reqs.Concat<object>(safes).Concat(shelters));
        }

        [HttpGet]
        public async Task<IActionResult> ExportRequestsCsv(DateTime? from, DateTime? to, RequestStatus? status)
        {
            var q = _db.UserRequests.AsQueryable();

            if (from.HasValue)
                q = q.Where(x => x.CreatedAt >= from.Value);
            if (to.HasValue)
                q = q.Where(x => x.CreatedAt <= to.Value);
            if (status.HasValue)
                q = q.Where(x => x.Status == status.Value);

            var list = await q.OrderBy(x => x.CreatedAt).ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("RequestCode,FullName,PhoneNumber,RiskLevel,Status,CreatedAt,Latitude,Longitude,AddressText");
            foreach (var r in list)
            {
             //   sb.AppendLine($"{r.RequestCode},{r.FullName},{r.PhoneNumber},{r.RiskLevel},{r.Status},{r.CreatedAt:u},{r.Latitude},{r.Longitude},"{r.AddressText}"");
            }

            var bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", "requests.csv");
        }
    }
}
