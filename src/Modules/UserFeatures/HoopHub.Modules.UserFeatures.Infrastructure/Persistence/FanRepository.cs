using HoopHub.BuildingBlocks.Infrastructure;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Fans;

namespace HoopHub.Modules.UserFeatures.Infrastructure.Persistence
{
    public class FanRepository(UserFeaturesContext context) : BaseRepository<Fan>(context), IFanRepository
    {
    }
}
