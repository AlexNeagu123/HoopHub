using HoopHub.BuildingBlocks.Application.Responses;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Players.GetBioByPlayerId
{
    public class GetBioByPlayerIdQuery : IRequest<Response<PlayerDto>>
    {
        public Guid PlayerId { get; set; }
    }
}
