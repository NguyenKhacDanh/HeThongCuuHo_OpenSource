using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RescueHub.Models.Enums;
using RescueHub.Models.Identity;

namespace RescueHub.Models.Entities
{
    public class UserRequest
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string RequestCode { get; set; } = default!;

        [Required, StringLength(200)]
        public string FullName { get; set; } = default!;

        [Required, Phone, StringLength(20)]
        public string PhoneNumber { get; set; } = default!;

        [Range(1, 100)]
        public int? PeopleCount { get; set; }

        public bool? HasChildren { get; set; }
        public bool? HasElderly { get; set; }
        public bool? NeedMedicalSupport { get; set; }
        public bool? HasPregnantWoman { get; set; }
        public bool? HasDisabledPerson { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(500)]
        public string? AddressText { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [Range(0, 1000)]
        public int? WaterLevel { get; set; }

        public RiskLevel RiskLevel { get; set; } = RiskLevel.Low;
        public RequestStatus Status { get; set; } = RequestStatus.New;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public string? AssignedRescuerId { get; set; }

        [ForeignKey(nameof(AssignedRescuerId))]
        public virtual ApplicationUser? AssignedRescuer { get; set; }

        [StringLength(2000)]
        public string? NotesFromRescuer { get; set; }

        public bool IsNightTime { get; set; }

        [StringLength(500)]
        public string? IncidentImagePath { get; set; }
    }
}
