using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Games.BoxScores.GetBoxScoresByPlayer
{
    public class GetBoxScoresByPlayerQuery : IRequest<Response<IReadOnlyList<LocalStoredBoxScoresDto>>>
    {
        public int GameCount { get; set; }
        public Guid PlayerId { get; set; }
    }
}
