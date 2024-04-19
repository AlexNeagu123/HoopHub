namespace HoopHub.Modules.UserAccess.IntegrationEvents
{
    public record UserRegisteredIntegrationEvent(Guid NotificationId, string UserId, string UserName, string UserEmail);
}