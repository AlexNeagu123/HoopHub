using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.NBAData.Domain.OutboxMessages;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace HoopHub.Modules.NBAData.Infrastructure.Interceptors
{
    public class NBADataConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
            InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;
            if (dbContext is null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var outboxMessages = dbContext.ChangeTracker.Entries<Entity>()
                .Select(x => x.Entity)
                .SelectMany(entry =>
                {
                    var domainEvents = entry.DomainEvents.ToArray();
                    entry.ClearDomainEvents();
                    return domainEvents;
                })
                .Select(domainEvent => new NBADataOutboxMessage
                {
                    Id = Guid.NewGuid(),
                    OccuredOnUtc = domainEvent.OccurredOn,
                    Type = domainEvent.GetType().Name,
                    Content = JsonConvert.SerializeObject(domainEvent,
                                               new JsonSerializerSettings
                                               {
                            TypeNameHandling = TypeNameHandling.All
                        })
                })
                .ToList();

            dbContext.Set<NBADataOutboxMessage>().AddRange(outboxMessages);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
