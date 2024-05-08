using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Threads.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserFeatures.Application.Threads
{
    public class TeamThreadVoteUpdatedDomainEventHandler(ILogger<TeamThreadVoteUpdatedDomainEventHandler> logger,
        ITeamThreadRepository threadCommentRepository,
        IFanRepository fanRepository) : INotificationHandler<TeamThreadVoteUpdatedDomainEvent>
    {
        private readonly ILogger<TeamThreadVoteUpdatedDomainEventHandler> _logger = logger;
        private readonly ITeamThreadRepository _threadCommentRepository = threadCommentRepository;
        private readonly IFanRepository _fanRepository = fanRepository;
        public async Task Handle(TeamThreadVoteUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var threadResult = await _threadCommentRepository.FindByIdAsyncIncludingFan(notification.ThreadId);
            if (!threadResult.IsSuccess)
                throw new DomainEventHandlerException($"Thread not found for vote update: {notification.ThreadId}");


            var thread = threadResult.Value;
            thread.UpdateVoteCount(notification.IsUpvote);

            var updateResult = await _threadCommentRepository.UpdateAsync(thread);
            if (!updateResult.IsSuccess)
                throw new DomainEventHandlerException($"Error updating thread vote: {notification.ThreadId}");

            var fan = thread.Fan;
            fan.UpdateVoteCount(notification.IsUpvote);

            var updateFanResult = await _fanRepository.UpdateAsync(fan);
            if (!updateFanResult.IsSuccess)
                throw new DomainEventHandlerException("Error updating fan vote");

            _logger.LogInformation($"Thread vote updated: {notification.ThreadId}");
        }
    }
}
