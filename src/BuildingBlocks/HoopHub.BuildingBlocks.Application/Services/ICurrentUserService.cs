using System.Security.Claims;

namespace HoopHub.BuildingBlocks.Application.Services
{
    public interface ICurrentUserService
    {
        string? GetUserRole { get; }
        string? GetUserId { get; }
        bool? GetUserLicense { get; }
        string? GetUserEmail { get; }
        ClaimsPrincipal GetCurrentClaimsPrincipal();
    }
}
