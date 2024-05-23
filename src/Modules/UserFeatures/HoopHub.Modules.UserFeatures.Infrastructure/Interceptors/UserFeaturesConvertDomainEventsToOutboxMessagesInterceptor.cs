using HoopHub.BuildingBlocks.Domain;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace HoopHub.Modules.UserFeatures.Infrastructure.Interceptors
{
    public class UserFeaturesConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
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
                .Select(domainEvent => new UserFeaturesOutboxMessage
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

            dbContext.Set<UserFeaturesOutboxMessage>().AddRange(outboxMessages);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
