using Microsoft.AspNetCore.Identity;
using RescueHub.Models.Entities;

namespace RescueHub.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual RescuerProfile? RescuerProfile { get; set; }
    }
}
