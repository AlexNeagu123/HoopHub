using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserAccess.Application.Services.Registration;
using HoopHub.Modules.UserAccess.Domain.ResetPassword;

namespace HoopHub.Modules.UserAccess.Application.Services.ResetPassword
{
    public interface IResetPasswordService
    {
        Task<BaseResponse> SendResetPasswordEmail(string? email);
        Task<Response<UserDto>> ResetPasswordAsync(ResetPasswordModel request);
    }
}