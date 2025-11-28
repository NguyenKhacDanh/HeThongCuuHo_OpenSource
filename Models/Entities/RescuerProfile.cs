using System.ComponentModel.DataAnnotations;

namespace RescueHub.Models.Entities
{
    public class RescuerProfile
    {
        [Key]
        public string Id { get; set; } = default!;

        [Required, StringLength(200)]
        public string FullName { get; set; } = default!;

        [Required, Phone, StringLength(20)]
        public string PhoneNumber { get; set; } = default!;

        [StringLength(200)]
        public string? Organization { get; set; }

        [StringLength(300)]
        public string? MainArea { get; set; }

        public double? AreaLatitude { get; set; }
        public double? AreaLongitude { get; set; }

        [Range(0, 2000)]
        public double? AreaRadiusKm { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
