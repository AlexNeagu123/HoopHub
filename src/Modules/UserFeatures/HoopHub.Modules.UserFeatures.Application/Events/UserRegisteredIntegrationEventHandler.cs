using HoopHub.Modules.UserAccess.IntegrationEvents;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Fans;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserFeatures.Application.Events
{
    public class UserRegisteredIntegrationEventHandler(ILogger<UserRegisteredIntegrationEventHandler> logger, IFanRepository fanRepository) : IConsumer<UserRegisteredIntegrationEvent>
    {
        private readonly ILogger<UserRegisteredIntegrationEventHandler> _logger = logger;
        private readonly IFanRepository _fanRepository = fanRepository;

        public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
        {
            _logger.LogInformation($"Integration Event Received. User with ID: {context.Message.UserId} has been registered.");
            var message = context.Message;
            
            var fanCreatedResult = Fan.Create(message.UserId, message.UserName, message.UserEmail);
            if (!fanCreatedResult.IsSuccess)
            {
                _logger.LogError(
                    $"Fan with ID: {message.UserId} could not be created. Reason: {fanCreatedResult.ErrorMsg}");
                return;
            }

            var fanAddedResult = await _fanRepository.AddAsync(fanCreatedResult.Value);
            if (!fanAddedResult.IsSuccess)
            {
                _logger.LogError(
                    $"Fan with ID: {message.UserId} could not be added to the database. Reason: {fanAddedResult.ErrorMsg}");
                return;
            }

            _logger.LogInformation($"Fan with ID: {message.UserId} has been added to the database.");
        }
    }
}
