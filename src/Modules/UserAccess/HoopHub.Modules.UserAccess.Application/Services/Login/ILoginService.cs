using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserAccess.Domain.Login;

namespace HoopHub.Modules.UserAccess.Application.Services.Login
{
    public interface ILoginService
    {
        Task<Response<string>> LoginAsync(LoginModel request);
    }
}
