using System.Security.Claims;
using HoopHub.BuildingBlocks.Application.Services;

namespace HoopHub.API.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        public string? GetUserRole => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
        public string? GetUserId => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        public bool? GetUserLicense => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Version) is "True";
        public string? GetUserEmail => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

        public ClaimsPrincipal GetCurrentClaimsPrincipal()
        {
            return _httpContextAccessor.HttpContext is { User: not null }
                ? _httpContextAccessor.HttpContext.User
                : null!;
        }
    }
}
