using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.UserFeatures.Domain.Threads.Events
{
    public class TeamThreadVoteAddedDomainEvent(Guid threadId, string fanId, bool isUpvote) : DomainEventBase
    {
        public Guid ThreadId { get; } = threadId;
        public string FanId { get; } = fanId;
        public bool IsUpvote { get; } = isUpvote;
    }
}
