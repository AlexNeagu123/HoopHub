using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.UserFeatures.Domain.Reviews.Events
{
    public class PlayerAverageRatingUpdatedDomainEvent(Guid playerId, decimal? averageRating) : DomainEventBase
    {
        public Guid PlayerId { get; } = playerId;
        public decimal? AverageRating { get; } = averageRating;
    }
}
