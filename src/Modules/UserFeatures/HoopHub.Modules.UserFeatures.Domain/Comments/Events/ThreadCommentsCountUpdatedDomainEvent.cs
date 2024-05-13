using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.UserFeatures.Domain.Comments.Events
{
    public class ThreadCommentsCountUpdatedDomainEvent(int delta, Guid? teamThreadId = null, Guid? gameThreadsId = null) : DomainEventBase
    {
        public Guid? TeamThreadId { get; } = teamThreadId;
        public Guid? GameThreadId { get; } = gameThreadsId;
        public int Delta { get; } = delta;
    }
}
