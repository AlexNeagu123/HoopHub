using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.UserFeatures.Domain.Comments.Events
{
    public class ReplyAddedToCommentDomainEvent(
        string recipientId,
        string senderId,
        string title,
        string content,
        string? senderImageUrl,
        string? attachedNavigationData) : DomainEventBase
    {
        public string RecipientId { get; } = recipientId;
        public string SenderId { get; } = senderId;
        public string Title { get; } = title;
        public string Content { get; } = content;
        public string? SenderImageUrl { get; } = senderImageUrl;
        public string? AttachedNavigationData { get; } = attachedNavigationData;
    }
}
