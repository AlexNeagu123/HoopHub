using HoopHub.BuildingBlocks.Domain;

namespace HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData
{
    public interface IBoxScoresDataService
    {
        Task<Result<IReadOnlyList<BoxScoreApiDto>>> GetBoxScoresAsyncByDate(string date);
        Task<Result<IReadOnlyList<BoxScoreApiDto>>> GetLiveBoxScores();
        Task<Result<BoxScoreApiDto>> GetBoxScoresAsyncByTeamAndDate(string date, int teamId);
    }
}
