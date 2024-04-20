using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Comments.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserFeatures.Application.Comments
{
    public class CommentVoteUpdatedDomainEventHandler(ILogger<CommentVoteUpdatedDomainEventHandler> logger,
        IThreadCommentRepository threadCommentRepository) : INotificationHandler<CommentVoteUpdatedDomainEvent>
    {
        private readonly ILogger<CommentVoteUpdatedDomainEventHandler> _logger = logger;
        private readonly IThreadCommentRepository _threadCommentRepository = threadCommentRepository;
        public async Task Handle(CommentVoteUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Comment vote updated for comment {CommentId}", notification.CommentId);
            var threadComment = await _threadCommentRepository.FindByIdAsync(notification.CommentId);
            if (!threadComment.IsSuccess)
            {
                _logger.LogError("Did not found comment with id {CommentId}", notification.CommentId);
                return;
            }

            threadComment.Value.UpdateVoteCount(notification.IsUpvote);
            var updateResult = await _threadCommentRepository.UpdateAsync(threadComment.Value);
            if (!updateResult.IsSuccess)
            {
                _logger.LogError("Failed to update vote count for comment {CommentId}", notification.CommentId);
                return;
            }

            _logger.LogInformation("Vote count updated for comment {CommentId}", notification.CommentId);
        }
    }
}
