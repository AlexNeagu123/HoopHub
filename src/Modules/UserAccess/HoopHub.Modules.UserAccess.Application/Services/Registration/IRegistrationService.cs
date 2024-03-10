using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserAccess.Domain.Registration;

namespace HoopHub.Modules.UserAccess.Application.Services.Registration
{
    public interface IRegistrationService
    {
        Task<Response<UserDto>> RegisterAsync(RegistrationModel request, string role);
    }
}
