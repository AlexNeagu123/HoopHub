using System.Security.Claims;
using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserAccess.Application.Services.Login;
using HoopHub.Modules.UserAccess.Domain.Login;
using HoopHub.Modules.UserAccess.Domain.Roles;
using HoopHub.Modules.UserAccess.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;

namespace HoopHub.Modules.UserAccess.Infrastructure.Services.Login
{
    public class LoginService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        : ILoginService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly JWTProvider _jwtProvider = new(configuration);

        public async Task<Response<string>> LoginAsync(LoginModel request)
        {
            var validator = new LoginValidator(_userManager);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Take(1).ToDictionary(error => error.PropertyName, error => error.ErrorMessage);
                return new Response<string>
                {
                    Success = false,
                    ValidationErrors = validationErrors
                };
            }

            var user = (await _userManager.FindByNameAsync(request.UserName))!;
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName!),
                new (ClaimTypes.NameIdentifier, user.Id),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
            var token = _jwtProvider.GenerateToken(authClaims);
            return new Response<string>
            {
                Success = true,
                Data = token
            };
        }
    }
}
