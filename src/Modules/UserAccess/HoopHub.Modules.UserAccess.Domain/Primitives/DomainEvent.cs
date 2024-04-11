using MediatR;

namespace HoopHub.Modules.UserAccess.Domain.Primitives
{
    public record DomainEvent(Guid id) : INotification;
}
