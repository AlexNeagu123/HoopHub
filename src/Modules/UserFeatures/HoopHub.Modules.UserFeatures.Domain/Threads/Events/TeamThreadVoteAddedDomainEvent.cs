using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.UserFeatures.Domain.Threads.Events
{
    public class TeamThreadVoteAddedDomainEvent(Guid threadId, bool isUpvote) : DomainEventBase
    {
        public Guid ThreadId { get; } = threadId;
        public bool IsUpvote { get; } = isUpvote;
    }
}
