using HoopHub.Modules.NBAData.Domain.Games.Events;
using HoopHub.Modules.NBAData.IntegrationEvents;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.NBAData.Application.Games
{
    public class GameCreatedDomainEventHandler(ILogger<GameCreatedDomainEventHandler> logger, IBus bus)
        : INotificationHandler<GameCreatedDomainEvent>
    {
        private readonly ILogger<GameCreatedDomainEventHandler> _logger = logger;
        private readonly IBus _bus = bus;

        public async Task Handle(GameCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[Domain Event Received] Game created: {GameId}", notification.Id);
            await _bus.Publish(new GameCreatedIntegrationEvent(
                Guid.NewGuid(),
                notification.Id,
                notification.Date,
                notification.HomeTeamId,
                notification.VisitorTeamId,
                notification.SeasonId,
                notification.HomeTeamScore,
                notification.VisitorTeamScore,
                notification.HomeTeamName,
                notification.VisitorTeamName,
                notification.HomeTeamImageUrl,
                notification.VisitorTeamImageUrl,
                notification.HomeTeamApiId,
                notification.VisitorTeamApiId,
                notification.Status,
                notification.Period,
                notification.Time,
                notification.Postseason
            ), cancellationToken);
        }
    }
}
