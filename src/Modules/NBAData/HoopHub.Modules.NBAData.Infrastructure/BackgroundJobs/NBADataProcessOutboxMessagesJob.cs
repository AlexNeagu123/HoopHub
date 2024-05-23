using HoopHub.BuildingBlocks.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;

namespace HoopHub.Modules.NBAData.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class NBADataProcessOutboxMessagesJob(NBADataContext context, IPublisher publisher) : IJob
    {
        private readonly NBADataContext _context = context;
        private readonly IPublisher _publisher = publisher;
        public async Task Execute(IJobExecutionContext context)
        {
            var messages = await _context.OutboxMessages
                .Where(x => x.ProcessedOnUtc == null)
                .Take(20)
                .ToListAsync(context.CancellationToken);

            foreach (var outboxMessage in messages)
            {
                var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

                if (domainEvent == null)
                    continue;

                try
                {
                    await _publisher.Publish(domainEvent, context.CancellationToken);
                    outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
                }
                catch (DomainEventHandlerException e)
                {
                    outboxMessage.Error = e.Message;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
