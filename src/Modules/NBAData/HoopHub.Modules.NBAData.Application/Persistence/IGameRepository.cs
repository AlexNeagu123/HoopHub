using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.NBAData.Domain.Games;

namespace HoopHub.Modules.NBAData.Application.Persistence
{
    public interface IGameRepository : IAsyncRepository<Game>
    {
        Task<Result<IReadOnlyList<Game>>> GetLastXGamesByTeam(Guid teamId, int lastCount);
        Task<Result<IReadOnlyList<Game>>> FindGamesByDate(DateTime date);
        Task<Result<Game>> FindByIdIncludingAll(Guid id);
        Task<Result<Game>> FindGameByDateAndTeams(DateTime date, Guid homeTeamId, Guid visitorTeamId);
    }
}
