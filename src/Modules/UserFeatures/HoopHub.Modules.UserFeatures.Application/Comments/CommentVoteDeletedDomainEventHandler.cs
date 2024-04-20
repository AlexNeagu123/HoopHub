using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Comments.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserFeatures.Application.Comments
{
    public class CommentVoteDeletedDomainEventHandler(ILogger<CommentVoteAddedDomainEventHandler> logger,
        IThreadCommentRepository threadCommentRepository) : INotificationHandler<CommentVoteDeletedDomainEvent>
    {
        private readonly ILogger<CommentVoteAddedDomainEventHandler> _logger = logger;
        private readonly IThreadCommentRepository _threadCommentRepository = threadCommentRepository;
        public async Task Handle(CommentVoteDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Comment vote deleted: {CommentId}", notification.CommentId);
            var threadComment = await _threadCommentRepository.FindByIdAsyncIncludingAll(notification.CommentId);
            if (!threadComment.IsSuccess)
            {
                _logger.LogError("Thread comment not found: {CommentId}", notification.CommentId);
                return;
            }

            var comment = threadComment.Value;
            comment.RemoveVote(notification.IsUpvote);

            var updateThreadCommentResult = await _threadCommentRepository.UpdateAsync(comment);
            if (!updateThreadCommentResult.IsSuccess)
                _logger.LogError("Thread comment vote not removed: {CommentId}", notification.CommentId);

            _logger.LogInformation("Thread comment vote removed: {CommentId}", notification.CommentId);
        }
    }
}
