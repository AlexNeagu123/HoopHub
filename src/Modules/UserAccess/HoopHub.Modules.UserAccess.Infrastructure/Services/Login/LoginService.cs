using System.Security.Claims;
using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserAccess.Application.Services.Login;
using HoopHub.Modules.UserAccess.Domain.Login;
using HoopHub.Modules.UserAccess.Domain.Users;
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
        private readonly JwtProvider _jwtProvider = new(configuration);

        public async Task<Response<string>> LoginAsync(LoginModel request)
        {
            var validator = new LoginValidator(_userManager);
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return Response<string>.ErrorResponseFromFluentResult(validationResult);

            var user = (await _userManager.FindByNameAsync(request.UserName))!;
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName!),
                new (ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email!),
                new(ClaimTypes.Version, user.IsLicensed.ToString()),
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
