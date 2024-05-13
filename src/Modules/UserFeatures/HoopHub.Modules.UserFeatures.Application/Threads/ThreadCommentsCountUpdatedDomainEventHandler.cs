using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Comments.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserFeatures.Application.Threads
{
    public class ThreadCommentsCountUpdatedDomainEventHandler(ILogger<ThreadCommentsCountUpdatedDomainEventHandler> logger,
        ITeamThreadRepository teamThreadRepository,
        IGameThreadRepository gameThreadRepository,
        IFanRepository fanRepository) : INotificationHandler<ThreadCommentsCountUpdatedDomainEvent>
    {
        private readonly ILogger<ThreadCommentsCountUpdatedDomainEventHandler> _logger = logger;
        private readonly ITeamThreadRepository _teamThreadRepository = teamThreadRepository;
        private readonly IGameThreadRepository _gameThreadRepository = gameThreadRepository;
        private readonly IFanRepository _fanRepository = fanRepository;

        public async Task Handle(ThreadCommentsCountUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "[Domain Event Received] comment count modified for thread {TeamThreadId} {GameThreadId}",
                notification.TeamThreadId, notification.GameThreadId);
            if (notification.TeamThreadId.HasValue)
            {
                var teamThreadResult = await _teamThreadRepository.FindByIdAsync(notification.TeamThreadId.Value);
                if (!teamThreadResult.IsSuccess)
                    throw new DomainEventHandlerException($"Team thread not found for comment count update: {notification.TeamThreadId}");

                var teamThread = teamThreadResult.Value;
                teamThread.UpdateCommentCount(notification.Delta);

                var updateResult = await _teamThreadRepository.UpdateAsync(teamThread);
                if (!updateResult.IsSuccess)
                    throw new DomainEventHandlerException($"Error updating team thread comment count: {notification.TeamThreadId}");

                _logger.LogInformation($"Team thread comment count updated: {notification.TeamThreadId}");
            }
            else if (notification.GameThreadId.HasValue)
            {
                var gameThreadResult = await _gameThreadRepository.FindByIdAsync(notification.GameThreadId.Value);
                if (!gameThreadResult.IsSuccess)
                    throw new DomainEventHandlerException($"Game thread not found for comment count update: {notification.GameThreadId}");

                var gameThread = gameThreadResult.Value;
                gameThread.UpdateCommentCount(notification.Delta);

                var updateResult = await _gameThreadRepository.UpdateAsync(gameThread);
                if (!updateResult.IsSuccess)
                    throw new DomainEventHandlerException($"Error updating game thread comment count: {notification.GameThreadId}");

                _logger.LogInformation($"Game thread comment count updated: {notification.GameThreadId}");
            }
            else
            {
                throw new DomainEventHandlerException("Thread id not found for comment count update");
            }

            var fanResult = await _fanRepository.FindByIdAsync(notification.FanId);
            if (!fanResult.IsSuccess)
                throw new DomainEventHandlerException($"Fan not found for comment count update: {notification.FanId}");

            fanResult.Value.UpdateCommentsCount(notification.Delta);

            var updateFanResult = await _fanRepository.UpdateAsync(fanResult.Value);
            if (!updateFanResult.IsSuccess)
                throw new DomainEventHandlerException($"Error updating fan comment count: {notification.FanId}");

            _logger.LogInformation($"Fan comment count updated: {notification.FanId}");
        }
    }
}
