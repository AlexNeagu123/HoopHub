namespace HoopHub.Modules.UserFeatures.IntegrationEvents
{
    public record PlayerAverageRatingUpdatedIntegrationEvent(Guid NotificationId, Guid PlayerId, decimal? AverageRating);
}
