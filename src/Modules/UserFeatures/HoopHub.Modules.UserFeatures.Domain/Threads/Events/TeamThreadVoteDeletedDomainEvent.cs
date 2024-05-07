using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.UserFeatures.Domain.Threads.Events
{
    public class TeamThreadVoteDeletedDomainEvent(Guid threadId, string fanId, bool isUpVote) : DomainEventBase
    {
        public Guid ThreadId { get; } = threadId;
        public string FanId { get; } = fanId;
        public bool IsUpvote { get; } = isUpVote;
    }
}
