using HoopHub.Modules.NBAData.IntegrationEvents;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using HoopHub.Modules.UserFeatures.Domain.FanNotifications;
using HoopHub.Modules.UserFeatures.Domain.Follows;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserFeatures.Application.FanNotifications.Events
{
    public class GameCreatedIntegrationEventHandler(ILogger<GameCreatedIntegrationEventHandler> logger,
        ITeamFollowEntryRepository teamFollowEntryRepository,
        INotificationRepository notificationRepository,
        IConfiguration configuration)
        : IConsumer<GameCreatedIntegrationEvent>
    {
        private readonly ILogger<GameCreatedIntegrationEventHandler> _logger = logger;
        private readonly ITeamFollowEntryRepository _teamFollowEntryRepository = teamFollowEntryRepository;
        private readonly INotificationRepository _notificationRepository = notificationRepository;
        private readonly IConfiguration _configuration = configuration;

        public async Task Consume(ConsumeContext<GameCreatedIntegrationEvent> context)
        {
            _logger.LogInformation($"Integration Event Received: Game Created for Game with ID: {context.Message.GameId}");
            var homeTeamFollowResult = await _teamFollowEntryRepository.GetAllByTeamIdIncludingFansAsync(context.Message.HomeTeamId);
            if (!homeTeamFollowResult.IsSuccess)
            {
                _logger.LogError($"Failed to get Fans following Home Team with ID: {context.Message.HomeTeamId}");
                return;
            }

            var visitorTeamFollowResult = await _teamFollowEntryRepository.GetAllByTeamIdIncludingFansAsync(context.Message.VisitorTeamId);
            if (!visitorTeamFollowResult.IsSuccess)
            {
                _logger.LogError($"Failed to get Fans following Visitor Team with ID: {context.Message.VisitorTeamId}");
                return;
            }

            var frontendUrl = _configuration["Urls:Frontend"];
            var homeTeamFollows = homeTeamFollowResult.Value;
            var visitorTeamFollows = visitorTeamFollowResult.Value;
            var gameLink = ClientRoutes.GetGameLink(context.Message.HomeTeamApiId,
                context.Message.VisitorTeamApiId, context.Message.Date.ToString("yyyy-MM-dd"), frontendUrl);

            await SendNotificationsByTeam(homeTeamFollows, context.Message.HomeTeamName, context.Message.HomeTeamImageUrl, gameLink);
            await SendNotificationsByTeam(visitorTeamFollows, context.Message.VisitorTeamName, context.Message.VisitorTeamImageUrl, gameLink);
        }

        private async Task SendNotificationsByTeam(IEnumerable<TeamFollowEntry> teamFollows, string teamName, string? teamImageUrl, string gameLink)
        {
            foreach (var fan in teamFollows.Select(tfe => tfe.Fan))
            {
                var notificationResult = Notification.Create(fan.Id, NotificationType.FollowedTeamGameEnd, Config.FollowedTeamGameEndsTitle,
                    Config.FollowedTeamGameEndsContent(teamName));

                if (!notificationResult.IsSuccess)
                {
                    _logger.LogError($"Failed to create Notification for Fan with ID: {fan.Id}");
                    continue;
                }

                var notification = notificationResult.Value;
                notification.AttachImageUrl(teamImageUrl);
                notification.AttachNavigationData(gameLink);

                var addResult = await _notificationRepository.AddAsync(notification);
                if (!addResult.IsSuccess)
                    _logger.LogError($"Failed to add Notification for Fan with ID: {fan.Id}");

            }
        }
    }
}