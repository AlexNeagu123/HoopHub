using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserAccess.Application.Services.Login;
using HoopHub.Modules.UserAccess.Domain.Login;

namespace HoopHub.Modules.UserAccess.Infrastructure.Services.Login
{
    public class LoginService : ILoginService
    {
        public Task<Response<string>> LoginAsync(LoginModel request)
        {
            throw new NotImplementedException();
        }
    }
}
