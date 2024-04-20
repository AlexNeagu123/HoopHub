using HoopHub.BuildingBlocks.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;

namespace HoopHub.Modules.UserFeatures.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxMessagesJob(UserFeaturesContext context, IPublisher publisher) : IJob
    {
        private readonly UserFeaturesContext _context = context;
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

                await _publisher.Publish(domainEvent, context.CancellationToken);
                outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }
    }
}
