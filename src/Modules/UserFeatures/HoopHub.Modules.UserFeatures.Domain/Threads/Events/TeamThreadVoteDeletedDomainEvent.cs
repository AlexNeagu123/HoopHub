using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.UserFeatures.Domain.Threads.Events
{
    public class TeamThreadVoteDeletedDomainEvent(Guid threadId, bool isUpVote) : DomainEventBase
    {
        public Guid ThreadId { get; } = threadId;
        public bool IsUpvote { get; } = isUpVote;
    }
}
