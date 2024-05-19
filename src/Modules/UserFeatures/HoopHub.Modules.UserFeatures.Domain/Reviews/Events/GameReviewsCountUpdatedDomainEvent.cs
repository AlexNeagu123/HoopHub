using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.UserFeatures.Domain.Reviews.Events
{
    public class GameReviewsCountUpdatedDomainEvent(int delta, string fanId) : DomainEventBase
    {
        public int Delta { get; } = delta;
        public string FanId { get; } = fanId;
    }
}
