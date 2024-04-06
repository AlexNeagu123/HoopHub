using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Games.Dtos;

namespace HoopHub.API.Hubs
{
    public interface ILiveBoxScoreClient
    {
        Task ReceiveMessage(string message);
        Task ReceiveLiveBoxScores(Response<IReadOnlyList<GameWithBoxScoreDto>> liveBoxScoreDto);
    }
}
