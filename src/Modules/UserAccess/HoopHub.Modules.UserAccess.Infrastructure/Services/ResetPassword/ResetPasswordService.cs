using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserAccess.Application.Constants;
using HoopHub.Modules.UserAccess.Application.Services.Emails;
using HoopHub.Modules.UserAccess.Application.Services.Registration;
using HoopHub.Modules.UserAccess.Application.Services.ResetPassword;
using HoopHub.Modules.UserAccess.Domain.ResetPassword;
using HoopHub.Modules.UserAccess.Domain.Users;
using HoopHub.Modules.UserAccess.Infrastructure.Services.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace HoopHub.Modules.UserAccess.Infrastructure.Services.ResetPassword
{
    public class ResetPasswordService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IEmailSender emailSender, ICurrentUserService currentUserService) : IResetPasswordService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly IConfiguration _configuration = configuration;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly UserMapper _userMapper = new();
        private readonly ICurrentUserService _currentUserService = currentUserService;
        public async Task<BaseResponse> SendResetPasswordEmail(string? email)
        {
            if (string.IsNullOrEmpty(email))
                email = _currentUserService.GetUserEmail!;

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BaseResponse.ErrorResponseFromKeyMessage(ErrorMessages.InvalidEmail, ValidationKeys.Email);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = System.Net.WebUtility.UrlEncode(token);
            var callbackUrl = _configuration["ProdUrl"] + $"/reset-password?email={email}&token={encodedToken}";
            var emailSendResult = await _emailSender.SendEmail("Reset Password", email, user.UserName!, $"Hi, {user.UserName!}. Click <a href='{callbackUrl}'>here</a> to reset your password.");
            if (emailSendResult == false)
                return BaseResponse.ErrorResponseFromKeyMessage(ErrorMessages.EmailNotSent, ValidationKeys.Email);

            return new BaseResponse
            {
                Success = true
            };
        }

        public async Task<Response<UserDto>> ResetPasswordAsync(ResetPasswordModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return Response<UserDto>.ErrorResponseFromKeyMessage(ErrorMessages.InvalidEmail, ValidationKeys.Email);

            if (request.Password != request.ConfirmPassword)
                return Response<UserDto>.ErrorResponseFromKeyMessage(ErrorMessages.PasswordsDoNotMatch, ValidationKeys.Password);

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (!resetPasswordResult.Succeeded)
                return new Response<UserDto>
                {
                    Success = false,
                    ValidationErrors = resetPasswordResult.Errors.Take(1)
                        .ToDictionary(_ => "Credentials", error => error.Description)
                };

            return new Response<UserDto>
            {
                Success = true,
                Data = _userMapper.UserToUserDto(user)
            };
        }
    }
}
