using Microsoft.AspNetCore.Identity;

namespace HoopHub.Modules.UserAccess.Domain.Users
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName  { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsLicensed { get; set; } = false;
    }
}
