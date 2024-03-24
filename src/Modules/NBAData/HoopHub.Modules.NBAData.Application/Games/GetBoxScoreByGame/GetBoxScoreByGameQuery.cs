using HoopHub.BuildingBlocks.Application.Responses;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Games.GetBoxScoreByGame
{
    public class GetBoxScoreByGameQuery : IRequest<Response<>>
    {
        public string Date { get; set; }
        public int HomeTeamApiId { get; set; }
        public int VisitorTeamApiId { get; set; }
    }
}
