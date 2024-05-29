using HoopHub.Modules.UserAccess.IntegrationEvents;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HoopHub.Modules.UserAccess.Domain.UserDetails
{
    public class UserDetailsChangedDomainEventHandler(IBus bus, ILogger<UserDetailsChangedDomainEventHandler> logger) : INotificationHandler<UserDetailsChangedDomainEvent>
    {
        private readonly IBus _bus = bus;
        private readonly ILogger<UserDetailsChangedDomainEventHandler> _logger = logger;

        public async Task Handle(UserDetailsChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User details changed event received");
            await _bus.Publish(new UserDetailsChangedIntegrationEvent(Guid.NewGuid(), notification.UserId, notification.UserName, notification.UserEmail, notification.IsLicensed),
                cancellationToken);
        }
    }
}
