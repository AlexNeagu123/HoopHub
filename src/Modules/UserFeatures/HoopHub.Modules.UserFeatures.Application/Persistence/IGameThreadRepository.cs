using HoopHub.BuildingBlocks.Application.Persistence;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Threads;

namespace HoopHub.Modules.UserFeatures.Application.Persistence
{
    public interface IGameThreadRepository : IAsyncRepository<GameThread>
    {
        Task<Result<GameThread>> FindByTupleIdAsync(Guid homeTeamId, Guid visitorTeamId, string date);
        Task<Result<IReadOnlyList<GameThread>>> GetAllByDateAsync(string date);
    }
}
