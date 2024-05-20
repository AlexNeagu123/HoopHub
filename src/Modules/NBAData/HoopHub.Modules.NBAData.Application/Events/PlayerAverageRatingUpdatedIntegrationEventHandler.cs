using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.UserFeatures.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.NBAData.Application.Events
{
    public class PlayerAverageRatingUpdatedIntegrationEventHandler(ILogger<PlayerAverageRatingUpdatedIntegrationEventHandler> logger, IPlayerRepository playerRepository) : IConsumer<PlayerAverageRatingUpdatedIntegrationEvent>
    {
        private readonly ILogger<PlayerAverageRatingUpdatedIntegrationEventHandler> _logger = logger;
        private readonly IPlayerRepository _playerRepository = playerRepository;

        public async Task Consume(ConsumeContext<PlayerAverageRatingUpdatedIntegrationEvent> context)
        {
            _logger.LogInformation($"Integration Event Received: Player Average Rating Updated for Player with ID: {context.Message.PlayerId}");
            var playerResult = await _playerRepository.FindByIdAsync(context.Message.PlayerId);
            if (!playerResult.IsSuccess)
            {
                _logger.LogError($"Player with ID: {context.Message.PlayerId} not found.");
                return;
            }

            var player = playerResult.Value;
            player.UpdateAverageRating(context.Message.AverageRating);

            var updateResult = await _playerRepository.UpdateAsync(player);
            if (!updateResult.IsSuccess)
            {
                _logger.LogError($"Failed to update Player with ID: {context.Message.PlayerId}.");
                return;
            }

            _logger.LogInformation($"Player with ID: {context.Message.PlayerId} updated with new Average Rating: {context.Message.AverageRating}");
        }
    }
}
