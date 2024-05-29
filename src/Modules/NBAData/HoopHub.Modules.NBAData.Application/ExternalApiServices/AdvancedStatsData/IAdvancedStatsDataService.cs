using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.NBAData.Application.ExternalApiServices.AdvancedStatsData
{
    public interface IAdvancedStatsDataService
    {
        Task<Result<IReadOnlyList<AdvancedStatsApiDto>>> GetAdvancedStatsAsyncByDate(string date);
    }
}
