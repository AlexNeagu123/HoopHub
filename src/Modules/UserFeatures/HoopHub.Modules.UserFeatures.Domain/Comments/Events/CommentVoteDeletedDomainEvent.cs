using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.UserFeatures.Domain.Comments.Events
{
    public class CommentVoteDeletedDomainEvent(Guid commentId, bool isUpVote) : DomainEventBase
    {
        public Guid CommentId { get; } = commentId;
        public bool IsUpvote { get; } = isUpVote;
    }
}
