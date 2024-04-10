using HoopHub.Modules.NBAData.Application.Seasons.Dtos;
using HoopHub.Modules.NBAData.Application.Teams.Dtos;

namespace HoopHub.Modules.NBAData.Application.Standings.Dtos
{
    public class StandingsEntryDto
    {
        public SeasonDto Season { get; set; }
        public TeamDto Team { get; set; }
        public int Rank { get; set; }
        public string Overall { get; set; } = string.Empty;
        public string Home { get; set; } = string.Empty;
        public string Road { get; set; } = string.Empty;
        public string EasternRecord { get; set; } = string.Empty;
        public string WesternRecord { get; set; } = string.Empty;
    }
}
