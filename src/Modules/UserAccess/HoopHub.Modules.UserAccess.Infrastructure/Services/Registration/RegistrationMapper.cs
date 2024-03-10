using HoopHub.Modules.UserAccess.Domain.Registration;
using HoopHub.Modules.UserAccess.Domain.Roles;
using Riok.Mapperly.Abstractions;

namespace HoopHub.Modules.UserAccess.Infrastructure.Services.Registration
{
    [Mapper]
    public partial class RegistrationMapper
    {
        public partial UserDto UserToUserDto(ApplicationUser user);
    }
}
