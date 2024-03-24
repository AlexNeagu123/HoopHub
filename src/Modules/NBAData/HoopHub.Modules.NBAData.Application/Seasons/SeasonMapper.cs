using HoopHub.Modules.NBAData.Application.Seasons.Dtos;
using HoopHub.Modules.NBAData.Domain.Seasons;
using Riok.Mapperly.Abstractions;

namespace HoopHub.Modules.NBAData.Application.Seasons
{
    [Mapper]
    public partial class SeasonMapper
    {
        public partial SeasonDto SeasonToSeasonDto(Season season);
    }
}
