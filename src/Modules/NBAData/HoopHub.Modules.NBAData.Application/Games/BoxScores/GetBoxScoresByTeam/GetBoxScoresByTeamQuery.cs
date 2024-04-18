using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Games.BoxScores.GetBoxScoresByTeam
{
    public class GetBoxScoresByTeamQuery : IRequest<Response<IReadOnlyList<GameWithBoxScoreDto>>>
    {
        public Guid TeamId { get; set; }
    }
}
