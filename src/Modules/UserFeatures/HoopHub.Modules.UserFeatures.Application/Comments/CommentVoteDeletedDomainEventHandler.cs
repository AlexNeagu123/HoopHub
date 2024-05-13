using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Comments.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserFeatures.Application.Comments
{
    public class CommentVoteDeletedDomainEventHandler(ILogger<CommentVoteDeletedDomainEvent> logger,
        IThreadCommentRepository threadCommentRepository,
        IFanRepository fanRepository) : INotificationHandler<CommentVoteDeletedDomainEvent>
    {
        private readonly ILogger<CommentVoteDeletedDomainEvent> _logger = logger;
        private readonly IThreadCommentRepository _threadCommentRepository = threadCommentRepository;
        private readonly IFanRepository _fanRepository = fanRepository;
        public async Task Handle(CommentVoteDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[Domain Event Received] Comment vote deleted: {CommentId}", notification.CommentId);
            var threadComment = await _threadCommentRepository.FindByIdAsyncIncludingAll(notification.CommentId);
            if (!threadComment.IsSuccess)
                throw new DomainEventHandlerException($"Thread comment not found: {notification.CommentId}");

            var comment = threadComment.Value;
            comment.RemoveVote(notification.IsUpvote);

            var updateThreadCommentResult = await _threadCommentRepository.UpdateAsync(comment);
            if (!updateThreadCommentResult.IsSuccess)
                throw new DomainEventHandlerException($"Thread comment vote not removed: {notification.CommentId}");

            var fanResult = await _fanRepository.FindByIdAsync(comment.FanId);
            if (!fanResult.IsSuccess)
                throw new DomainEventHandlerException($"Fan not found: {comment.FanId}");

            var fan = fanResult.Value;
            fan.RemoveVote(notification.IsUpvote);

            var updateFanResult = await _fanRepository.UpdateAsync(fan);
            if (!updateFanResult.IsSuccess)
                throw new DomainEventHandlerException($"Fan vote not removed: {comment.FanId}");

            _logger.LogInformation("Thread comment vote removed: {CommentId}", notification.CommentId);
        }
    }
}
