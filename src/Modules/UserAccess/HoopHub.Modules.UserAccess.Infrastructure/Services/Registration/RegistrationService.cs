using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserAccess.Application.Services.Registration;
using HoopHub.Modules.UserAccess.Domain.Registration;
using HoopHub.Modules.UserAccess.Domain.Roles;
using Microsoft.AspNetCore.Identity;

namespace HoopHub.Modules.UserAccess.Infrastructure.Services.Registration
{
    public class RegistrationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        : IRegistrationService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly RegistrationMapper _userMapper = new();
        public async Task<Response<UserDto>> RegisterAsync(RegistrationModel request, string role)
        {
            var validator = new RegistrationValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.ToDictionary(error => error.PropertyName, error => error.ErrorMessage);
                return new Response<UserDto>
                {
                    Success = false,
                    ValidationErrors = validationErrors
                };
            }

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
                    ValidationErrors = createResult.Errors.Take(1).ToDictionary(error => "Password", error => error.Description)
                };
            }

            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
            await _userManager.AddToRoleAsync(user, role);
            
            return new Response<UserDto>
            {
                Success = true,
                Data = _userMapper.UserToUserDto(user)
            };
        }
    }
}
