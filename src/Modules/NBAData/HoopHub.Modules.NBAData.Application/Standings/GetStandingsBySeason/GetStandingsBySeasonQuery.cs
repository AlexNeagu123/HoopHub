using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Standings.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Standings.GetStandingsBySeason
{
    public class GetStandingsBySeasonQuery : IRequest<Response<List<StandingsEntryDto>>>
    { 
        public int Season { get; set; }
    }
}
