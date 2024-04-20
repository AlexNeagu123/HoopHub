using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.UserFeatures.Domain.Comments.Events
{
    public class CommentVoteAddedDomainEvent(Guid commentId, string fanId, bool isUpvote) : DomainEventBase
    {
        public Guid CommentId { get; } = commentId;
        public string FanId { get; } = fanId;
        public bool IsUpvote { get; } = isUpvote;
    }
}
