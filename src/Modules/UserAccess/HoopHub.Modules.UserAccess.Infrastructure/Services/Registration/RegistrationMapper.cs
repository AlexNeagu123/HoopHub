using HoopHub.Modules.UserAccess.Application.Services.Registration;
using HoopHub.Modules.UserAccess.Domain.Users;
using Riok.Mapperly.Abstractions;

namespace HoopHub.Modules.UserAccess.Infrastructure.Services.Registration
{
    [Mapper]
    public partial class RegistrationMapper
    {
        public partial UserDto UserToUserDto(ApplicationUser user);
    }
}
