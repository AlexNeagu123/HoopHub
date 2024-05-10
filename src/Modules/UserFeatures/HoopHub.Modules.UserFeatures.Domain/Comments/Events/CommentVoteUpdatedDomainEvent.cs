using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.UserFeatures.Domain.Comments.Events
{
    public class CommentVoteUpdatedDomainEvent(Guid commentId, bool isUpvote) : DomainEventBase
    {
        public Guid CommentId { get; } = commentId;
        public bool IsUpvote { get; } = isUpvote;
    }
}
