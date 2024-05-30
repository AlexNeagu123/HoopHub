using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Games.BoxScores.GetRecentBoxScores
{
    public class GetRecentBoxScoresQuery : IRequest<Response<IReadOnlyList<LocalStoredBoxScoresDto>>>
    {

    }
}
