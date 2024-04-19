using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Fans;
using HoopHub.Modules.UserFeatures.Domain.Rules;

namespace HoopHub.Modules.UserFeatures.Domain.FanNotifications
{
    public class Notification : AuditableEntity
    {
        public Guid Id { get; private set; }
        public string RecipientId { get; private set; }
        public Fan Recipient { get; private set; } = null!;
        public string? SenderId { get; private set; }
        public Fan? Sender { get; private set; }
        public NotificationType Type { get; private set; }
        public bool IsRead { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public string? AttachedImageUrl { get; private set; }

        private Notification(string recipientId, NotificationType type, string title, string content)
        {
            Id = Guid.NewGuid();
            RecipientId = recipientId;
            Type = type;
            Title = title;
            Content = content;
            IsRead = false;
        }

        public static Result<Notification> Create(string recipientId, NotificationType type, string title,
            string content)
        {
            try
            {
                CheckRule(new RecipientIdCannotBeEmpty(recipientId));
                CheckRule(new TitleMustBeValid(title));
                CheckRule(new ThreadContentMustBeValid(content));
            }
            catch (BusinessRuleValidationException e)
            {
                return Result<Notification>.Failure(e.Details);
            }
            
            return Result<Notification>.Success(new Notification(recipientId, type, title, content));
        }

        public void MarkAsRead()
        {
            IsRead = true;
        }

        public void AttachSender(string senderId)
        {
            try
            {
                CheckRule(new SenderIdCannotBeEmpty(senderId));
                SenderId = senderId;
            }
            catch (BusinessRuleValidationException) { }
        }

        public void AttachImageUrl(string imageUrl)
        {
            try
            {
                CheckRule(new ImageUrlCannotBeEmpty(imageUrl));
                AttachedImageUrl = imageUrl;
            } catch(BusinessRuleValidationException) { }
        }
    }
}
