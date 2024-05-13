using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.UserFeatures.Domain.Comments.Events
{
    public class ThreadCommentRepliesCountUpdatedDomainEvent(int delta, Guid commentId) : DomainEventBase
    {
        public int Delta { get; } = delta;
        public Guid CommentId { get; } = commentId;
    }
}
