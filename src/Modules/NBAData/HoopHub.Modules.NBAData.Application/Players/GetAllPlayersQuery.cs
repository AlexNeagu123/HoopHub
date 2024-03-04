using HoopHub.BuildingBlocks.Application.Responses;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Players
{
    public class GetAllPlayersQuery : IRequest<Response<List<PlayerDto>>>
    {
    }
}
