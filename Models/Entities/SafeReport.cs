using System.ComponentModel.DataAnnotations;

namespace RescueHub.Models.Entities
{
    public class SafeReport
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string FullName { get; set; } = default!;

        [Required, Phone, StringLength(20)]
        public string PhoneNumber { get; set; } = default!;

        [StringLength(500)]
        public string? AddressText { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [StringLength(1000)]
        public string? Note { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
