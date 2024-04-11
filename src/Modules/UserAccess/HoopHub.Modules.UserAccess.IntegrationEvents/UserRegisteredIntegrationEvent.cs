namespace HoopHub.Modules.UserAccess.IntegrationEvents
{
    public sealed record UserRegisteredIntegrationEvent(Guid NotificationId, string UserId, string UserName, string UserEmail);
}