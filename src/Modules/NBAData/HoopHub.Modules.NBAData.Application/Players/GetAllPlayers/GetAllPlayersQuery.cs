using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Players.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Players.GetAllPlayers
{
    public class GetAllPlayersQuery : IRequest<Response<IReadOnlyList<PlayerDto>>>
    {
    }
}
