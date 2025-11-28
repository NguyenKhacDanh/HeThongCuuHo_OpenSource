using System.ComponentModel.DataAnnotations;

namespace RescueHub.ViewModels.Account
{
    public class RegisterRescuerViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = default!;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [Required]
        public string FullName { get; set; } = default!;

        [Required, Phone]
        public string PhoneNumber { get; set; } = default!;

        public string? Organization { get; set; }
        public string? MainArea { get; set; }

        public double? AreaLatitude { get; set; }
        public double? AreaLongitude { get; set; }
        public double? AreaRadiusKm { get; set; }
    }

    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = default!;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        public bool RememberMe { get; set; }
    }
}
