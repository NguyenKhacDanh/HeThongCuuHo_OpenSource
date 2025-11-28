using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RescueHub.Models.Entities;
using RescueHub.Models.Identity;

namespace RescueHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserRequest> UserRequests => Set<UserRequest>();
        public DbSet<SafeReport> SafeReports => Set<SafeReport>();
        public DbSet<RescuerProfile> RescuerProfiles => Set<RescuerProfile>();
        public DbSet<NotificationLog> NotificationLogs => Set<NotificationLog>();
        public DbSet<Shelter> Shelters => Set<Shelter>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasOne(u => u.RescuerProfile)
                .WithOne()
                .HasForeignKey<RescuerProfile>(r => r.Id);

            builder.Entity<UserRequest>()
                .Property(x => x.RiskLevel)
                .HasConversion<int>();

            builder.Entity<UserRequest>()
                .Property(x => x.Status)
                .HasConversion<int>();

            builder.Entity<UserRequest>()
                .HasIndex(x => new { x.Status, x.RiskLevel });
        }
    }
}
