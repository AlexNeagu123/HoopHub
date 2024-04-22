using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Comments.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserFeatures.Application.Comments
{
    public class CommentVoteUpdatedDomainEventHandler(ILogger<CommentVoteUpdatedDomainEventHandler> logger,
        IThreadCommentRepository threadCommentRepository,
        IFanRepository fanRepository) : INotificationHandler<CommentVoteUpdatedDomainEvent>
    {
        private readonly ILogger<CommentVoteUpdatedDomainEventHandler> _logger = logger;
        private readonly IThreadCommentRepository _threadCommentRepository = threadCommentRepository;
        private readonly IFanRepository _fanRepository = fanRepository;
        public async Task Handle(CommentVoteUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[Domain Event Received] Comment vote updated for comment {CommentId}", notification.CommentId);
            var threadComment = await _threadCommentRepository.FindByIdAsyncIncludingAll(notification.CommentId);
            if (!threadComment.IsSuccess)
                throw new DomainEventHandlerException($"Did not found comment with id {notification.CommentId}");


            threadComment.Value.UpdateVoteCount(notification.IsUpvote);
            var updateResult = await _threadCommentRepository.UpdateAsync(threadComment.Value);
            if (!updateResult.IsSuccess)
                throw new DomainEventHandlerException($"Failed to update vote count for comment {notification.CommentId}");

            var fanResult = await _fanRepository.FindByIdAsync(threadComment.Value.FanId);
            if (!fanResult.IsSuccess)
                throw new DomainEventHandlerException($"Did not found fan with id {threadComment.Value.FanId}");

            fanResult.Value.UpdateVoteCount(notification.IsUpvote);
            var updateFanResult = await _fanRepository.UpdateAsync(fanResult.Value);
            if (!updateFanResult.IsSuccess)
                throw new DomainEventHandlerException($"Failed to update vote count for fan {threadComment.Value.FanId}");

            _logger.LogInformation("Vote count updated for comment {CommentId}", notification.CommentId);
        }
    }
}
