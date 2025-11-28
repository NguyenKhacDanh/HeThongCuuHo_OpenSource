using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RescueHub.Models.Entities
{
    public class NotificationLog
    {
        public int Id { get; set; }

        public int UserRequestId { get; set; }

        [ForeignKey(nameof(UserRequestId))]
        public virtual UserRequest UserRequest { get; set; } = default!;

        [Required, StringLength(200)]
        public string RecipientPhoneOrEmail { get; set; } = default!;

        [Required, StringLength(2000)]
        public string Message { get; set; } = default!;

        [Required, StringLength(50)]
        public string NotificationType { get; set; } = "InApp";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
