using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Playoffs.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Playoffs.GetPlayoffSeriesBySeason
{
    public class GetPlayoffSeriesBySeasonQuery : IRequest<Response<GroupedPlayoffSeriesDto>>
    {
        public int Season { get; set; }
    }
}
