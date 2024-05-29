using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.UserAccess.Domain.UserDetails
{
    public class UserDetailsChangedDomainEvent(string userId, string userName, string userEmail, bool isLicensed) : DomainEventBase
    {
        public string UserId { get; } = userId;
        public string UserName { get; } = userName;
        public string UserEmail { get; } = userEmail;
        public bool IsLicensed { get; } = isLicensed;
    }
}
