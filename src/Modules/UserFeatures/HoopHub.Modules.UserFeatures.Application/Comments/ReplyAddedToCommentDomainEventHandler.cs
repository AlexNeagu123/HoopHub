using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Comments.Events;
using HoopHub.Modules.UserFeatures.Domain.FanNotifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserFeatures.Application.Comments
{
    public class ReplyAddedToCommentDomainEventHandler(
        ILogger<ReplyAddedToCommentDomainEventHandler> logger,
        INotificationRepository notificationRepository)
        : INotificationHandler<ReplyAddedToCommentDomainEvent>
    {
        private readonly ILogger<ReplyAddedToCommentDomainEventHandler> _logger = logger;
        private readonly INotificationRepository _notificationRepository = notificationRepository;

        public async Task Handle(ReplyAddedToCommentDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ReplyAddedToCommentDomainEvent received..");
            var fanNotificationResult = Notification.Create(notification.RecipientId, NotificationType.CommentReply, notification.Title, notification.Content);
            if (!fanNotificationResult.IsSuccess)
                throw new DomainEventHandlerException("Notification could not be created in domain event handler..");

            var fanNotification = fanNotificationResult.Value;

            fanNotification.AttachImageUrl(notification.SenderImageUrl);
            fanNotification.AttachSender(notification.SenderId);
            fanNotification.AttachNavigationData(notification.AttachedNavigationData);

            var addResult = await _notificationRepository.AddAsync(fanNotification);
            if (!addResult.IsSuccess)
                throw new DomainEventHandlerException("Notification could not be added in domain event handler..");

            _logger.LogInformation("Notification created successfully for reply added to comment..");
        }
    }
}
