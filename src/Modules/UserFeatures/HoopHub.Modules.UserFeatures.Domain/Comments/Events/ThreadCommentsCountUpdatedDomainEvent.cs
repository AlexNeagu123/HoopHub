using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.UserFeatures.Domain.Comments.Events
{
    public class ThreadCommentsCountUpdatedDomainEvent(int delta, string fanId, Guid? teamThreadId = null, Guid? gameThreadsId = null) : DomainEventBase
    {
        public Guid? TeamThreadId { get; } = teamThreadId;
        public Guid? GameThreadId { get; } = gameThreadsId;
        public string FanId { get; } = fanId;
        public int Delta { get; } = delta;
    }
}
