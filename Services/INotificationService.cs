using RescueHub.Models.Entities;

namespace RescueHub.Services
{
    public interface INotificationService
    {
        Task NotifyUserAsync(UserRequest request, string message, string type = "InApp");
    }
}
