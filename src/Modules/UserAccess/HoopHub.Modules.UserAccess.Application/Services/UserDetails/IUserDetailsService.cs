using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserAccess.Application.Services.Registration;
using HoopHub.Modules.UserAccess.Domain.UserDetails;

namespace HoopHub.Modules.UserAccess.Application.Services.UserDetails
{
    public interface IUserDetailsService
    {
        Task<Response<UserDto>> UpdateUserDetails(UserDetailsModel userDetails);
    }
}
