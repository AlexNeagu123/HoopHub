using HoopHub.BuildingBlocks.Application.Responses;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Teams.GetBioByTeamId
{
    public class GetBioByTeamIdQuery : IRequest<Response<TeamDto>>
    {
        public Guid TeamId { get; set; }
    }
}
