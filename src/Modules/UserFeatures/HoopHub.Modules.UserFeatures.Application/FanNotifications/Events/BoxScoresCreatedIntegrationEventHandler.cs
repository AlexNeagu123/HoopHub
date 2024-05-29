using HoopHub.Modules.NBAData.IntegrationEvents;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using HoopHub.Modules.UserFeatures.Domain.FanNotifications;
using HoopHub.Modules.UserFeatures.Domain.Follows;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserFeatures.Application.FanNotifications.Events
{
    public class BoxScoresCreatedIntegrationEventHandler(ILogger<BoxScoresCreatedIntegrationEventHandler> logger,
        IPlayerFollowEntryRepository playerFollowEntryRepository,
        INotificationRepository notificationRepository)
        : IConsumer<BoxScoresCreatedIntegrationEvent>
    {
        private readonly ILogger<BoxScoresCreatedIntegrationEventHandler> _logger = logger;
        private readonly IPlayerFollowEntryRepository _playerFollowEntryRepository = playerFollowEntryRepository;
        private readonly INotificationRepository _notificationRepository = notificationRepository;

        public async Task Consume(ConsumeContext<BoxScoresCreatedIntegrationEvent> context)
        {
            _logger.LogInformation($"Integration Event Received: Box Score Created for Player with ID: {context.Message.PlayerId}");

            var notificationMessage = IsGoodGame(context.Message, context.Message.PlayerName);
            if (notificationMessage == null)
                return;

            var playerFollowResult = await _playerFollowEntryRepository.GetAllByPlayerIdIncludingFansAsync(context.Message.PlayerId);
            if (!playerFollowResult.IsSuccess)
            {
                _logger.LogError($"Failed to get Fans following Player with ID: {context.Message.PlayerId}");
                return;
            }

            var playerFollows = playerFollowResult.Value;
            var gameLink = ClientRoutes.GetGameLink(context.Message.HomeTeamApiId,
                context.Message.VisitorTeamApiId, context.Message.Date.ToString("yyyy-MM-dd"));

            await SendNotificationsByPlayer(playerFollows, context.Message.PlayerId, context.Message.PlayerImageUrl, gameLink, notificationMessage);
        }

        private async Task SendNotificationsByPlayer(IEnumerable<PlayerFollowEntry> playerFollows, Guid playerId, string? playerImageUrl, string gameLink, string notificationMessage)
        {
            foreach (var fan in playerFollows.Select(pfe => pfe.Fan))
            {
                var notificationResult = Notification.Create(fan.Id, NotificationType.FollowedPlayerGoodPerformance, Config.FollowedPlayerGoodGameTitle,
                    notificationMessage);

                if (!notificationResult.IsSuccess)
                {
                    _logger.LogError($"Failed to create Notification for Fan with ID: {fan.Id}");
                    continue;
                }

                var notification = notificationResult.Value;
                notification.AttachNavigationData(gameLink);
                notification.AttachImageUrl(playerImageUrl);

                var addResult = await _notificationRepository.AddAsync(notification);
                if (!addResult.IsSuccess)
                    _logger.LogError($"Failed to add Notification for Fan with ID: {fan.Id}");
            }
        }

        private static string? IsGoodGame(BoxScoresCreatedIntegrationEvent boxScore, string playerName)
        {
            var doubleDigitsCount = 0;

            if (boxScore.Pts >= 10)
                doubleDigitsCount++;
            if (boxScore.Reb >= 10)
                doubleDigitsCount++;
            if (boxScore.Ast >= 10)
                doubleDigitsCount++;
            if (boxScore.Stl >= 10)
                doubleDigitsCount++;
            if (boxScore.Blk >= 10)
                doubleDigitsCount++;

            var messagePrefix = $"{playerName} had an ";
            if (doubleDigitsCount >= 3)
                return messagePrefix + "Impressive Triple Double game tonight";

            if (boxScore.Reb > 10 || boxScore.Blk > 3 || boxScore.Stl > 3)
                return messagePrefix + "Impressive Defensive Game tonight";

            if (doubleDigitsCount >= 2)
                return messagePrefix + "Impressive Double Double game tonight";


            return boxScore switch
            {
                { Pts: > 20, FgPct: > 0.5 } => messagePrefix + "Impressive and efficient scoring game tonight",
                { Fg3a: > 10, Fg3Pct: > 0.5 } => messagePrefix + "Impressive 3 point shooting game tonight",
                _ => null
            };
        }
    }
}
