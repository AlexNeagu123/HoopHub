using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Teams.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Teams.GetTeamById
{
    public class GetTeamByIdQuery : IRequest<Response<TeamDto>>
    {
        public Guid TeamId { get; set; }
    }
}
