using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Players.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Players.GetAllActivePlayers
{
    public class GetAllActivePlayersQuery : IRequest<Response<IReadOnlyList<PlayerDto>>>
    {
    }
}
