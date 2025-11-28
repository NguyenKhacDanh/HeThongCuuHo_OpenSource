using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RescueHub.Models.Entities;
using RescueHub.Models.Identity;

namespace RescueHub.Data
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            await _db.Database.EnsureCreatedAsync();

            string[] roles = { "Admin", "Rescuer" };
            foreach (var role in roles)
            {
                if (!await _roleManager.Roles.AnyAsync(r => r.Name == role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminEmail = "admin@rescuehub.local";
            var admin = await _userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(admin, "Admin@123");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, "Admin");
                }
            }

            if (!await _userManager.Users.AnyAsync(u => u.RescuerProfile != null))
            {
                var rescuer = new ApplicationUser
                {
                    UserName = "rescue1@rescuehub.local",
                    Email = "rescue1@rescuehub.local",
                    EmailConfirmed = true
                };
                var create = await _userManager.CreateAsync(rescuer, "Rescue@123");
                if (create.Succeeded)
                {
                    await _userManager.AddToRoleAsync(rescuer, "Rescuer");

                    _db.RescuerProfiles.Add(new RescuerProfile
                    {
                        Id = rescuer.Id,
                        FullName = "Đội cứu hộ Quận 1",
                        PhoneNumber = "0900000001",
                        Organization = "Quân đội",
                        MainArea = "Quận 1, TP.HCM",
                        AreaLatitude = 10.777,
                        AreaLongitude = 106.700,
                        AreaRadiusKm = 10,
                        IsActive = true
                    });
                    await _db.SaveChangesAsync();
                }
            }
        }
    }
}
