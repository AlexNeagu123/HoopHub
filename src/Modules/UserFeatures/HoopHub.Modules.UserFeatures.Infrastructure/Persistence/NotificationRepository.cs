using HoopHub.BuildingBlocks.Domain;
using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.FanNotifications;
using Microsoft.EntityFrameworkCore;

namespace HoopHub.Modules.UserFeatures.Infrastructure.Persistence
{
    public class NotificationRepository(UserFeaturesContext context) : BaseRepository<Notification>(context), INotificationRepository
    {
        public async Task<PagedResult<IReadOnlyList<Notification>>> GetByFanIdPagedAsync(string fanId, int pageNumber, int pageSize)
        {
            var notifications = await context.Notifications
                .Include(x => x.Sender)
                .Include(x => x.Recipient)
                .Where(x => x.RecipientId == fanId)
                .OrderByDescending(x => x.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalResults = await context.Notifications
                .CountAsync(x => x.RecipientId == fanId);

            return PagedResult<IReadOnlyList<Notification>>.Success(notifications, totalResults);
        }

        public async Task<PagedResult<IReadOnlyList<Notification>>> GetByFanIdAndIsReadFlagPagedAsync(string fanId, int pageNumber, int pageSize, bool isRead)
        {
            var notifications = await context.Notifications
                .Include(x => x.Sender)
                .Include(x => x.Recipient)
                .Where(x => x.RecipientId == fanId && x.IsRead == isRead)
                .OrderByDescending(x => x.CreatedDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalResults = context.Notifications
                .Count(x => x.RecipientId == fanId && x.IsRead == isRead);

            return PagedResult<IReadOnlyList<Notification>>.Success(notifications, totalResults);
        }
    }
}
