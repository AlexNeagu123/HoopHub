using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.GetAdvancedStatsEntriesByGame
{
    public class GetAdvancedStatsByGameQuery : IRequest<Response<IReadOnlyList<LocalStoredAdvancedStatsEntryDto>>>
    {
        public string Date { get; set; } = string.Empty;
        public int HomeTeamApiId { get; set; }
        public int VisitorTeamApiId { get; set; }
    }
}
