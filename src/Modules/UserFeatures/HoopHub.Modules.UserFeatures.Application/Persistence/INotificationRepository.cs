using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.FanNotifications;

namespace HoopHub.Modules.UserFeatures.Application.Persistence
{
    public interface INotificationRepository : IAsyncRepository<Notification>
    {
        Task<PagedResult<IReadOnlyList<Notification>>> GetByFanIdPagedAsync(string fanId, int pageNumber, int pageSize);
        Task<PagedResult<IReadOnlyList<Notification>>> GetByFanIdAndIsReadFlagPagedAsync(string fanId, int pageNumber, int pageSize, bool isRead);
    }
}
