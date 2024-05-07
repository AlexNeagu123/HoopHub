﻿using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Threads.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserFeatures.Application.Threads
{
    public class TeamThreadVoteDeletedDomainEventHandler(ILogger<TeamThreadVoteAddedDomainEventHandler> logger,
        ITeamThreadRepository threadCommentRepository,
        IFanRepository fanRepository) : INotificationHandler<TeamThreadVoteAddedDomainEvent>
    {
        private readonly ILogger<TeamThreadVoteAddedDomainEventHandler> _logger = logger;
        private readonly ITeamThreadRepository _threadCommentRepository = threadCommentRepository;
        private readonly IFanRepository _fanRepository = fanRepository;

        public async Task Handle(TeamThreadVoteAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            var threadResult = await _threadCommentRepository.FindByIdAsyncIncludingFan(notification.ThreadId);
            if (!threadResult.IsSuccess)
                throw new DomainEventHandlerException($"Thread not found for vote deleted event. ThreadId: {notification.ThreadId}");

            var thread = threadResult.Value;
            thread.RemoveVote(notification.IsUpvote);

            var updateThreadResult = await _threadCommentRepository.UpdateAsync(thread);
            if (!updateThreadResult.IsSuccess)
                throw new DomainEventHandlerException($"Error updating thread for vote deleted event. ThreadId: {notification.ThreadId}");

            var fanResult = await _fanRepository.FindByIdAsync(notification.FanId);
            if (!fanResult.IsSuccess)
                throw new DomainEventHandlerException($"Fan not found for vote deleted event. FanId: {notification.FanId}");

            var fan = fanResult.Value;
            fan.RemoveVote(notification.IsUpvote);

            var updateFanResult = await _fanRepository.UpdateAsync(fan);
            if (!updateFanResult.IsSuccess)
                throw new DomainEventHandlerException($"Error updating fan for vote deleted event. FanId: {notification.FanId}");

            _logger.LogInformation("Vote removed from thread. ThreadId: {ThreadId}, FanId: {FanId}", notification.ThreadId, notification.FanId);
        }
    }
}
