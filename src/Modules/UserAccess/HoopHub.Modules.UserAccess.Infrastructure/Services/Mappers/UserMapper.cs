using HoopHub.Modules.UserAccess.Application.Services.Registration;
using HoopHub.Modules.UserAccess.Domain.Users;
using Riok.Mapperly.Abstractions;

namespace HoopHub.Modules.UserAccess.Infrastructure.Services.Mappers
{
    [Mapper]
    public partial class UserMapper
    {
        public partial UserDto UserToUserDto(ApplicationUser user);
    }
}
