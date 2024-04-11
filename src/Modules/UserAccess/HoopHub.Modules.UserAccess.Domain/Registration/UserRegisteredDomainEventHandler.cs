//using HoopHub.Modules.UserAccess.IntegrationEvents;
//using MediatR;
//using Rebus.Bus;

//namespace HoopHub.Modules.UserAccess.Domain.Registration
//{
//    public class UserRegisteredDomainEventHandler(IBus bus) : INotificationHandler<UserRegisteredDomainEvent>
//    {
//        private readonly IBus _bus = bus;
//        public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
//        {
//            await _bus.Send(new UserRegisteredIntegrationEvent(notification.Id, notification.UserId, notification.UserName, notification.UserEmail));
//        }
//    }
//}
