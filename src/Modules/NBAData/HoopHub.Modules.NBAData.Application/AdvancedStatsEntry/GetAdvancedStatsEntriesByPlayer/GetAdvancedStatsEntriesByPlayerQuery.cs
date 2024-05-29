using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.GetAdvancedStatsEntriesByPlayer
{
    public class GetAdvancedStatsEntriesByPlayerQuery : IRequest<Response<IReadOnlyList<LocalStoredAdvancedStatsEntryDto>>>
    {
        public Guid PlayerId { get; set; }
    }
}
