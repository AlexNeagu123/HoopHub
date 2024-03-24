using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.PlayerTeamSeasons.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.PlayerTeamSeasons.GetPlayerTeamHistory
{
    public class GetPlayerTeamHistoryQuery : IRequest<Response<IReadOnlyList<PlayerTeamSeasonDto>>>
    {
        public Guid PlayerId { get; set; }
    }
}
