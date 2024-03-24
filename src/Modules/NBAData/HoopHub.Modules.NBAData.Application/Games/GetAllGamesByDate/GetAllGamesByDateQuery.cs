using HoopHub.BuildingBlocks.Application.Responses;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Games.GetAllGamesByDate
{
    public class GetAllGamesByDateQuery : IRequest<Response<IReadOnlyList<GameDto>>>
    {
        public string Date { get; set; } = string.Empty;
    }
}
