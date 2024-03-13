using HoopHub.BuildingBlocks.Application.Responses;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.PlayerTeamSeasons.GetPlayerTeamHistory
{
    public class GetPlayerTeamHistoryQuery : IRequest<Response<IReadOnlyList<PlayerTeamSeasonDto>>>
    {
        public Guid PlayerId { get; set; }
    }
}
