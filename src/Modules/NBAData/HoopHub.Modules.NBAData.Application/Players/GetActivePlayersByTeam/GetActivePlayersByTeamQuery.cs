using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Players.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Players.GetActivePlayersByTeam
{
    public class GetActivePlayersByTeamQuery : IRequest<Response<IReadOnlyList<PlayerDto>>>
    {
        public Guid TeamId { get; set;  }
    }
}
