using HoopHub.Modules.NBAData.Domain.BoxScores.Events;
using HoopHub.Modules.NBAData.IntegrationEvents;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.NBAData.Application.Games.BoxScores
{
    public class BoxScoresCreatedDomainEventHandler(ILogger<BoxScoresCreatedDomainEventHandler> logger, IBus bus)
        : INotificationHandler<BoxScoresCreatedDomainEvent>
    {
        private readonly ILogger<BoxScoresCreatedDomainEventHandler> _logger = logger;
        private readonly IBus _bus = bus;

        public async Task Handle(BoxScoresCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("[Domain Event Received] Box score created: {BoxScoreId}", notification.GameId);

            await _bus.Publish(new BoxScoresCreatedIntegrationEvent(
                Guid.NewGuid(),
                notification.GameId,
                notification.PlayerId,
                notification.TeamId,
                notification.PlayerName,
                notification.Date,
                notification.PlayerImageUrl,
                notification.HomeTeamApiId,
                notification.VisitorTeamApiId,
                notification.Min,
                notification.Fgm,
                notification.Fga,
                notification.FgPct,
                notification.Fg3m,
                notification.Fg3a,
                notification.Fg3Pct,
                notification.Ftm,
                notification.Fta,
                notification.FtPct,
                notification.Oreb,
                notification.Dreb,
                notification.Reb,
                notification.Ast,
                notification.Stl,
                notification.Blk,
                notification.Turnover,
                notification.Pf,
                notification.Pts
            ), cancellationToken);
        }
    }
}