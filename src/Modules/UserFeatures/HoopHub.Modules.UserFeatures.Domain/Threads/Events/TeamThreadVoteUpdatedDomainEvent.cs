namespace HoopHub.Modules.UserFeatures.Domain.Threads.Events
{
    public class TeamThreadVoteUpdatedDomainEvent(Guid threadId, string fanId, bool isUpVote)
    {
        public Guid ThreadId { get; } = threadId;
        public string FanId { get; } = fanId;
        public bool IsUpvote { get; } = isUpVote;
    }
}
