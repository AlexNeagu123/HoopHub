using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.NBAData.Application.ExternalApiServices.GamesData
{
    public interface IGamesDataService
    {
        Task<Result<IReadOnlyList<GameApiDto>>> GetGamesByDateAsync(string date);
    }
}
