using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Comments.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserFeatures.Application.Comments
{
    public class CommentVoteAddedDomainEventHandler(ILogger<CommentVoteAddedDomainEventHandler> logger,
        IThreadCommentRepository threadCommentRepository,
        IFanRepository fanRepository) : INotificationHandler<CommentVoteAddedDomainEvent>
    {
        private readonly ILogger<CommentVoteAddedDomainEventHandler> _logger = logger;
        private readonly IThreadCommentRepository _threadCommentRepository = threadCommentRepository;
        private readonly IFanRepository _fanRepository = fanRepository;
        public async Task Handle(CommentVoteAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[Domain Event Received] Comment vote added for comment {CommentId} by fan {FanId}", notification.CommentId, notification.FanId);
            var commentResult = await _threadCommentRepository.FindByIdAsyncIncludingAll(notification.CommentId);
            if (!commentResult.IsSuccess)   
                throw new DomainEventHandlerException($"Comment {notification.CommentId} not found");

            var comment = commentResult.Value;
            if (notification.IsUpvote)
                comment.UpVote();
            else
                comment.DownVote();

            var updateResult = await _threadCommentRepository.UpdateAsync(comment);
            if (!updateResult.IsSuccess)
                throw new DomainEventHandlerException($"Failed to update comment {notification.CommentId}");


            var fanResult = await _fanRepository.FindByIdAsync(comment.FanId);
            if (!fanResult.IsSuccess)
                throw new DomainEventHandlerException($"Fan {comment.FanId} not found");

            var fan = fanResult.Value;
            if (notification.IsUpvote)
                fan.UpVote();
            else
                fan.DownVote();

            var updateFanResult = await _fanRepository.UpdateAsync(fan);
            if (!updateFanResult.IsSuccess)
                throw new DomainEventHandlerException($"Failed to update fan {comment.FanId}");

            _logger.LogInformation("Comment {CommentId} and Fan {FanId} updated", notification.CommentId, comment.FanId);
        }
    }
}
