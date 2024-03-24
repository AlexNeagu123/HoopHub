using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Teams.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Teams.GetAllTeams
{
    public class GetAllTeamsQuery : IRequest<Response<IReadOnlyList<TeamDto>>>
    {
    }
}
