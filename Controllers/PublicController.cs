using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RescueHub.Data;
using RescueHub.Models.Entities;
using RescueHub.Services;
using RescueHub.ViewModels.Public;

namespace RescueHub.Controllers
{
    public class PublicController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IRiskCalculatorService _risk;
        private readonly IWebHostEnvironment _env;

        public PublicController(ApplicationDbContext db, IRiskCalculatorService risk, IWebHostEnvironment env)
        {
            _db = db;
            _risk = risk;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RequestHelp()
        {
            return View(new CreateUserRequestViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequestHelp(CreateUserRequestViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.FullName) ||
                string.IsNullOrWhiteSpace(model.PhoneNumber))
            {
                ModelState.AddModelError("", "Vui lòng nhập Họ tên và Số điện thoại.");
            }

            //if (!ModelState.IsValid)
            //    return View(model);

            var request = new UserRequest
            {
                FullName = model.FullName!,
                PhoneNumber = model.PhoneNumber!,
                PeopleCount = model.PeopleCount,
                HasChildren = model.HasChildren,
                HasElderly = model.HasElderly,
                NeedMedicalSupport = model.NeedMedicalSupport,
                HasPregnantWoman = model.HasPregnantWoman,
                HasDisabledPerson = model.HasDisabledPerson,
                WaterLevel = model.WaterLevel,
                Description = model.Description,
                AddressText = model.AddressText,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                CreatedAt = DateTime.UtcNow,
                IsNightTime = IsNightNowInVN()
            };

            if (model.IncidentImage != null && model.IncidentImage.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploads);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.IncidentImage.FileName)}";
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await model.IncidentImage.CopyToAsync(stream);
                }

                request.IncidentImagePath = "/uploads/" + fileName;
            }

            request.RequestCode = await GenerateRequestCodeAsync();
            request.RiskLevel = _risk.Calculate(request);

            _db.UserRequests.Add(request);
            await _db.SaveChangesAsync();

            ViewBag.RequestCode = request.RequestCode;
            return View("RequestHelpSuccess", request);
        }

        [HttpGet]
        public IActionResult SafeReport()
        {
            return View(new CreateSafeReportViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SafeReport(CreateSafeReportViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.FullName) ||
                string.IsNullOrWhiteSpace(model.PhoneNumber))
            {
                ModelState.AddModelError("", "Vui lòng nhập Họ tên và Số điện thoại.");
            }

            if (!ModelState.IsValid)
                return View(model);

            var safe = new SafeReport
            {
                FullName = model.FullName!,
                PhoneNumber = model.PhoneNumber!,
                AddressText = model.AddressText,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Note = model.Note,
                CreatedAt = DateTime.UtcNow
            };

            _db.SafeReports.Add(safe);
            await _db.SaveChangesAsync();

            return View("SafeReportSuccess", safe);
        }

        private async Task<string> GenerateRequestCodeAsync()
        {
            var today = DateTime.UtcNow.Date;
            var countToday = await _db.UserRequests
                .CountAsync(x => x.CreatedAt.Date == today);
            var running = (countToday + 1).ToString("D4");
            return $"REQ-{today:yyyyMMdd}-{running}";
        }

        private bool IsNightNowInVN()
        {
            var now = DateTime.UtcNow.AddHours(7);
            return now.Hour >= 19 || now.Hour <= 5;
        }
    }
}
