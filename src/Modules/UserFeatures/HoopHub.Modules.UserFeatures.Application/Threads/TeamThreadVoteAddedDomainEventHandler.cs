using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Threads.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserFeatures.Application.Threads
{
    public class TeamThreadVoteAddedDomainEventHandler(ILogger<TeamThreadVoteAddedDomainEventHandler> logger,
        ITeamThreadRepository threadCommentRepository,
        IFanRepository fanRepository) : INotificationHandler<TeamThreadVoteAddedDomainEvent>
    {
        private readonly ILogger<TeamThreadVoteAddedDomainEventHandler> _logger = logger;
        private readonly ITeamThreadRepository _threadCommentRepository = threadCommentRepository;
        private readonly IFanRepository _fanRepository = fanRepository;

        public async Task Handle(TeamThreadVoteAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[Domain Event Received] Team thread vote added for comment {ThreadId} by fan {FanId}",
                notification.ThreadId, notification.FanId);
            var threadResult = await _threadCommentRepository.FindByIdAsyncIncludingFan(notification.ThreadId);
            if (!threadResult.IsSuccess)
                throw new DomainEventHandlerException($"Thread {notification.ThreadId} not found");

            var thread = threadResult.Value;
            if (notification.IsUpvote)
                thread.UpVote();
            else
                thread.DownVote();

            var updateResult = await _threadCommentRepository.UpdateAsync(thread);
            if (!updateResult.IsSuccess)
                throw new DomainEventHandlerException($"Error updating thread {notification.ThreadId}");

            var fanResult = await _fanRepository.FindByIdAsync(notification.FanId);
            if (!fanResult.IsSuccess)
                throw new DomainEventHandlerException($"Fan {notification.FanId} not found");

            var fan = fanResult.Value;
            if (notification.IsUpvote)
                fan.UpVote();
            else
                fan.DownVote();

            var updateFanResult = await _fanRepository.UpdateAsync(fan);
            if (!updateFanResult.IsSuccess)
                throw new DomainEventHandlerException($"Failed to update fan {thread.FanId}");

            _logger.LogInformation(
                "[Domain Event Processed] Team thread vote added for comment {ThreadId} by fan {FanId}",
                notification.ThreadId, fan.Id);
        }
    }
}
