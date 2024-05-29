using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Games.BoxScores.GetBoxScoresByTeam
{
    public class GetBoxScoresByTeamQuery : IRequest<Response<IReadOnlyList<LocalStoredGameDto>>>
    {
        public int GameCount { get; set; }
        public Guid TeamId { get; set; }
    }
}
