using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Comments.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserFeatures.Application.Comments
{
    public class ThreadCommentRepliesCountUpdatedDomainEventHandler(IThreadCommentRepository threadCommentRepository, ILogger<ThreadCommentRepliesCountUpdatedDomainEventHandler> logger)
        : INotificationHandler<ThreadCommentRepliesCountUpdatedDomainEvent>
    {
        private readonly IThreadCommentRepository _threadCommentRepository = threadCommentRepository;
        private readonly ILogger<ThreadCommentRepliesCountUpdatedDomainEventHandler> _logger = logger;

        public async Task Handle(ThreadCommentRepliesCountUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[Domain Event Received] reply count modified for comment {CommentId}", notification.CommentId);
            var threadComment = await _threadCommentRepository.FindByIdAsyncIncludingAll(notification.CommentId);
            if (!threadComment.IsSuccess)
                throw new DomainEventHandlerException($"Thread comment not found: {notification.CommentId}");

            var comment = threadComment.Value;
            comment.UpdateRepliesCount(notification.Delta);

            var updateThreadCommentResult = await _threadCommentRepository.UpdateAsync(comment);
            if (!updateThreadCommentResult.IsSuccess)
                throw new DomainEventHandlerException($"Thread comment update failed: {notification.CommentId}");

            _logger.LogInformation($"Comment reply count updated: {notification.CommentId}");
        }
    }
}
