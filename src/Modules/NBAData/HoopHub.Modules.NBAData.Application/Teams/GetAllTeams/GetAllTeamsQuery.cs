using HoopHub.BuildingBlocks.Application.Responses;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Teams.GetAllTeams
{
    public class GetAllTeamsQuery : IRequest<Response<IReadOnlyList<TeamDto>>>
    {
    }
}
