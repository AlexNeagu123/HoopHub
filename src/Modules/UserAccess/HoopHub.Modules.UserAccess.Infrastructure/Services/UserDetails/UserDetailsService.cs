using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserAccess.Application.Constants;
using HoopHub.Modules.UserAccess.Application.Services.Registration;
using HoopHub.Modules.UserAccess.Application.Services.UserDetails;
using HoopHub.Modules.UserAccess.Domain.UserDetails;
using HoopHub.Modules.UserAccess.Domain.Users;
using HoopHub.Modules.UserAccess.Infrastructure.Services.Mappers;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HoopHub.Modules.UserAccess.Infrastructure.Services.UserDetails
{
    public class UserDetailsService(UserManager<ApplicationUser> userManager, IPublisher publisher, ICurrentUserService currentUserService) : IUserDetailsService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IPublisher _publisher = publisher;
        private readonly UserMapper _userMapper = new();

        public async Task<Response<UserDto>> UpdateUserDetails(UserDetailsModel userDetails)
        {
            var requestUserId = _currentUserService.GetUserId!;
            var user = await _userManager.FindByIdAsync(requestUserId);
            if (user == null)
                return Response<UserDto>.ErrorResponseFromKeyMessage(ErrorMessages.UserNotFound, ValidationKeys.Email);


            if (!string.IsNullOrEmpty(userDetails.UserName))
            {
                if (userDetails.UserName.Length > Config.UserNameMaxLength)
                    return Response<UserDto>.ErrorResponseFromKeyMessage(ErrorMessages.UserNameTooLong, ValidationKeys.Username);

                var userNameExistsResult = await _userManager.FindByNameAsync(userDetails.UserName);
                if (userNameExistsResult != null)
                    return Response<UserDto>.ErrorResponseFromKeyMessage(ErrorMessages.UserAlreadyExists, ValidationKeys.Username);

                user.UserName = userDetails.UserName;
            }

            user.IsLicensed = userDetails.IsLicensed;
            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
                return new Response<UserDto>
                {
                    Success = false,
                    ValidationErrors = updateResult.Errors.Take(1)
                        .ToDictionary(_ => "UserDetails", error => error.Description)
                };

            await _publisher.Publish(new UserDetailsChangedDomainEvent(user.Id, user.UserName!, user.Email!, user.IsLicensed));

            return new Response<UserDto>
            {
                Success = true,
                Data = _userMapper.UserToUserDto(user)
            };
        }
    }
}
