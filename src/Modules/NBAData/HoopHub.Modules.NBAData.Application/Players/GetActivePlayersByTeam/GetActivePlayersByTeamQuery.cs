using HoopHub.BuildingBlocks.Application.Responses;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Players.GetActivePlayersByTeam
{
    public class GetActivePlayersByTeamQuery : IRequest<Response<IReadOnlyList<PlayerDto>>>
    {
        public Guid TeamId { get; set;  }
    }
}
