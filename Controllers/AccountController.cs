using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RescueHub.Data;
using RescueHub.Models.Entities;
using RescueHub.Models.Identity;
using RescueHub.ViewModels.Account;

namespace RescueHub.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _db;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public IActionResult RegisterRescuer() => View(new RegisterRescuerViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterRescuer(RegisterRescuerViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var e in result.Errors)
                    ModelState.AddModelError("", e.Description);
                return View(model);
            }

            await _userManager.AddToRoleAsync(user, "Rescuer");

            _db.RescuerProfiles.Add(new RescuerProfile
            {
                Id = user.Id,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Organization = model.Organization,
                MainArea = model.MainArea,
                AreaLatitude = model.AreaLatitude,
                AreaLongitude = model.AreaLongitude,
                AreaRadiusKm = model.AreaRadiusKm,
                IsActive = true
            });
            await _db.SaveChangesAsync();

            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Dashboard", "Rescuer");
        }

        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(
                model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
                return Redirect(returnUrl ?? Url.Action("Dashboard", "Rescuer")!);

            ModelState.AddModelError("", "Đăng nhập không thành công.");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Public");
        }
    }
}
