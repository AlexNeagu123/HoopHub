using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Reviews.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserFeatures.Application.Reviews.GameReviews
{
    public class GameReviewsCountUpdatedDomainEventHandler(ILogger<GameReviewsCountUpdatedDomainEventHandler> logger,
        IFanRepository fanRepository) : INotificationHandler<GameReviewsCountUpdatedDomainEvent>
    {
        private readonly ILogger<GameReviewsCountUpdatedDomainEventHandler> _logger = logger;
        private readonly IFanRepository _fanRepository = fanRepository;
        public async Task Handle(GameReviewsCountUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                $"[Domain Event Received] reviews count modified for fan {notification.FanId}");

            var fanResult = await _fanRepository.FindByIdAsync(notification.FanId);
            if (!fanResult.IsSuccess)
            {
                _logger.LogWarning(
                                       $"[Domain Event Error] fan with id {notification.FanId} not found");
                return;
            }

            var fan = fanResult.Value;
            fan.UpdateReviewsCount(notification.Delta);
            
            var updateResult = await _fanRepository.UpdateAsync(fan);
            if (!updateResult.IsSuccess)
            {
                _logger.LogWarning(
                                       $"[Domain Event Error] failed to update reviews count for fan {notification.FanId}");
            }

            _logger.LogInformation(
                               $"[Domain Event Processed] reviews count modified for fan {notification.FanId}");
        }
    }
}
