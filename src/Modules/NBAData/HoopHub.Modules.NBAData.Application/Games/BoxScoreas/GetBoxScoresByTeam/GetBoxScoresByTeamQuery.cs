using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Games.GetGamesByTeam
{
    public class GetBoxScoresByTeamQuery : IRequest<Response<IReadOnlyList<GameWithBoxScoreDto>>>
    {
        public int ApiId { get; set; }
        public int GameCount { get; set; }
    }
}
