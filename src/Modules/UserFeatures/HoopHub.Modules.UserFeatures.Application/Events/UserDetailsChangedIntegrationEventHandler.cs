using HoopHub.Modules.UserAccess.IntegrationEvents;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserFeatures.Application.Events
{
    public class UserDetailsChangedIntegrationEventHandler(ILogger<UserDetailsChangedIntegrationEventHandler> logger, IFanRepository fanRepository) : IConsumer<UserDetailsChangedIntegrationEvent>
    {
        private readonly IFanRepository _fanRepository = fanRepository;
        private readonly ILogger<UserDetailsChangedIntegrationEventHandler> _logger = logger;

        public async Task Consume(ConsumeContext<UserDetailsChangedIntegrationEvent> context)
        {
            _logger.LogInformation("User details changed integration event received");
            var userResult = await _fanRepository.FindByIdAsync(context.Message.UserId);
            if (!userResult.IsSuccess)
            {
                _logger.LogError("Fan with id {UserId} was not found", context.Message.UserId);
                return;
            }

            var user = userResult.Value;
            user.UpdateDetails(context.Message.UserName, context.Message.IsLicensed);

            var updateResult = await _fanRepository.UpdateAsync(user);
            if (!updateResult.IsSuccess)
            {
                _logger.LogError("Failed to update fan with id {UserId}", context.Message.UserId);
            }
        }
    }
}
