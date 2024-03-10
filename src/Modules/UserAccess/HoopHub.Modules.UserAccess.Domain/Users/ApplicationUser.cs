using Microsoft.AspNetCore.Identity;

namespace HoopHub.Modules.UserAccess.Domain.Roles
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName  { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
