using RescueHub.Data;
using RescueHub.Models.Entities;

namespace RescueHub.Services
{
    public class FakeNotificationService : INotificationService
    {
        private readonly ApplicationDbContext _db;

        public FakeNotificationService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task NotifyUserAsync(UserRequest request, string message, string type = "InApp")
        {
            var log = new NotificationLog
            {
                UserRequestId = request.Id,
                RecipientPhoneOrEmail = request.PhoneNumber,
                Message = message,
                NotificationType = type,
                CreatedAt = DateTime.UtcNow
            };

            _db.NotificationLogs.Add(log);
            await _db.SaveChangesAsync();
        }
    }
}
