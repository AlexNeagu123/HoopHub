using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.NBAData.Application.ExternalApiServices.SeasonAverageStats
{
    public interface ISeasonAverageStatsService
    {
        Task<Result<SeasonAverageStatsDto>> GetAverageStatsBySeasonIdAndPlayerIdAsync(string season, int playerApiId);
    }
}
