using System.ComponentModel.DataAnnotations;

namespace RescueHub.Models.Entities
{
    public class Shelter
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; } = default!;

        [StringLength(500)]
        public string? AddressText { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [Range(0, 100000)]
        public int? Capacity { get; set; }

        [Range(0, 100000)]
        public int? CurrentPeople { get; set; }

        public bool? HasFood { get; set; }
        public bool? HasMedicine { get; set; }
        public bool? HasElectricity { get; set; }

        [StringLength(1000)]
        public string? Note { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
