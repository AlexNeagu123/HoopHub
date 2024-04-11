using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Fans;

namespace HoopHub.Modules.UserFeatures.Application.Persistence
{
    public interface IFanRepository : IAsyncRepository<Fan>
    {
    }
}
