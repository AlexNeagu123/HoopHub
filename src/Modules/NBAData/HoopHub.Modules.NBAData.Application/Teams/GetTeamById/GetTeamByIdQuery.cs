using HoopHub.BuildingBlocks.Application.Responses;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Teams.GetTeamById
{
    public class GetTeamByIdQuery : IRequest<Response<TeamDto>>
    {
        public Guid TeamId { get; set; }
    }
}
