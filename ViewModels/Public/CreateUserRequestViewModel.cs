using Microsoft.AspNetCore.Http;

namespace RescueHub.ViewModels.Public
{
    public class CreateUserRequestViewModel
    {
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public int? PeopleCount { get; set; }
        public bool? HasChildren { get; set; }
        public bool? HasElderly { get; set; }
        public bool? NeedMedicalSupport { get; set; }
        public bool? HasPregnantWoman { get; set; }
        public bool? HasDisabledPerson { get; set; }
        public int? WaterLevel { get; set; }
        public string? Description { get; set; }
        public string? AddressText { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public IFormFile? IncidentImage { get; set; }
    }

    public class CreateSafeReportViewModel
    {
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AddressText { get; set; }
        public string? Note { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
