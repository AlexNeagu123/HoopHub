using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.NBAData.Domain.Seasons;

namespace HoopHub.Modules.NBAData.Application.Persistence
{
    public interface ISeasonRepository : IAsyncRepository<Season>
    {
        Task<Result<Season>> FindBySeasonPeriod(string seasonPeriod);
    }
}
