using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Games.BoxScores.GetBoxScoreByGame
{
    public class GetBoxScoreByGameQuery : IRequest<Response<GameWithBoxScoreDto>>
    {
        public string Date { get; set; } = string.Empty;
        public int HomeTeamApiId { get; set; }
        public int VisitorTeamApiId { get; set; }
    }
}
