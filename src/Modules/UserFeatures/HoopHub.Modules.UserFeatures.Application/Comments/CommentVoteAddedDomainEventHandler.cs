using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Comments.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserFeatures.Application.Comments
{
    public class CommentVoteAddedDomainEventHandler(ILogger<CommentVoteAddedDomainEventHandler> logger, IThreadCommentRepository threadCommentRepository) : INotificationHandler<CommentVoteAddedDomainEvent>
    {
        private readonly ILogger<CommentVoteAddedDomainEventHandler> _logger = logger;
        private readonly IThreadCommentRepository _threadCommentRepository = threadCommentRepository;
        public async Task Handle(CommentVoteAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[Domain Event Received] Comment vote added for comment {CommentId} by fan {FanId}", notification.CommentId, notification.FanId);
            var commentResult = await _threadCommentRepository.FindByIdAsync(notification.CommentId);
            if (!commentResult.IsSuccess)
            {
                _logger.LogWarning("Comment {CommentId} not found", notification.CommentId);
                return;
            }

            var comment = commentResult.Value;
            if (notification.IsUpvote)
                comment.UpVote();
            else
                comment.DownVote();

            var updateResult = await _threadCommentRepository.UpdateAsync(comment);
            if (!updateResult.IsSuccess)
                _logger.LogError("Failed to update comment {CommentId}", notification.CommentId);
        }
    }
}
