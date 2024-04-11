using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserAccess.Application.Constants;
using HoopHub.Modules.UserAccess.Application.Services.Registration;
using HoopHub.Modules.UserAccess.Domain.Registration;
using HoopHub.Modules.UserAccess.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace HoopHub.Modules.UserAccess.Infrastructure.Services.Registration
{
    public class RegistrationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IPublisher publisher)
        : IRegistrationService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IPublisher _publisher = publisher;
        private readonly RegistrationMapper _userMapper = new();
        public async Task<Response<UserDto>> RegisterAsync(RegistrationModel request, string role)
        {
            var validator = new RegistrationValidator(_userManager);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return Response<UserDto>.ErrorResponseFromFluentResult(validationResult);

            ApplicationUser user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                Email = request.Email,
            };

            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                return new Response<UserDto>
                {
                    Success = false,
                    ValidationErrors = createResult.Errors.Take(1).ToDictionary(error => ValidationKeys.Password, error => error.Description)
                };
            }

            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }

            await _userManager.AddToRoleAsync(user, role);

            await _publisher.Publish(new UserRegisteredDomainEvent(new Guid(), user.Id, user.UserName, user.Email));

            return new Response<UserDto>
            {
                Success = true,
                Data = _userMapper.UserToUserDto(user)
            };
        }
    }
}
