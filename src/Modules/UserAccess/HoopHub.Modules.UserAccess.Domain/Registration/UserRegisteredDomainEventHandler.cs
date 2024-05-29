using HoopHub.Modules.UserAccess.IntegrationEvents;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserAccess.Domain.Registration
{
    public class UserRegisteredDomainEventHandler(IBus bus, ILogger<UserRegisteredDomainEventHandler> logger) : INotificationHandler<UserRegisteredDomainEvent>
    {
        private readonly IBus _bus = bus;
        private readonly ILogger<UserRegisteredDomainEventHandler> _logger = logger;
        public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Integration Event Sent. User with ID: {notification.UserId} has been registered.");
            await _bus.Publish(new UserRegisteredIntegrationEvent(notification.Id, notification.UserId, notification.UserName, notification.UserEmail, notification.IsLicensed), cancellationToken);
        }
    }
}
