using HoopHub.Modules.UserFeatures.Domain.Reviews.Events;
using HoopHub.Modules.UserFeatures.IntegrationEvents;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.PlayerPerformanceReviews
{
    public class PlayerAverageRatingUpdatedDomainEventHandler(IBus bus, ILogger<PlayerAverageRatingUpdatedDomainEventHandler> logger) : INotificationHandler<PlayerAverageRatingUpdatedDomainEvent>
    {
        private readonly IBus _bus = bus;
        private readonly ILogger<PlayerAverageRatingUpdatedDomainEventHandler> _logger = logger;

        public async Task Handle(PlayerAverageRatingUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Player with ID: {notification.PlayerId} has a new average rating of {notification.AverageRating}");
            await _bus.Publish(
                new PlayerAverageRatingUpdatedIntegrationEvent(Guid.NewGuid(), notification.PlayerId,
                    notification.AverageRating), cancellationToken);
        }
    }
}
