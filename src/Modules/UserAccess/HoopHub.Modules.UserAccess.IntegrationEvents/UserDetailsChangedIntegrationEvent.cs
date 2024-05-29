namespace HoopHub.Modules.UserAccess.IntegrationEvents
{
    public record UserDetailsChangedIntegrationEvent(
        Guid NotificationId,
        string UserId,
        string UserName,
        string UserEmail,
        bool IsLicensed);
}
