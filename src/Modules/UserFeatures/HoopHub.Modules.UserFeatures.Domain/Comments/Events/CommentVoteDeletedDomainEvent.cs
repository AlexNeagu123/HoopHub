using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.UserFeatures.Domain.Comments.Events
{
    public class CommentVoteDeletedDomainEvent(Guid commentId, string fanId, bool isUpVote) : DomainEventBase
    {
        public Guid CommentId { get; } = commentId;
        public string FanId { get; } = fanId;
        public bool IsUpvote { get; } = isUpVote;
    }
}
