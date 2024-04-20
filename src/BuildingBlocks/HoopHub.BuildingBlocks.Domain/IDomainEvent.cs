using MediatR;

namespace HoopHub.BuildingBlocks.Domain
{
    public interface IDomainEvent : INotification
    {
        Guid Id { get; }
        DateTime OccurredOn { get; }
    }
}
