using HoopHub.Modules.NBAData.Application.Seasons.Dtos;

namespace HoopHub.Modules.NBAData.Application.TeamBios.Dtos
{
    public class TeamBioDto
    {
        public SeasonDto? Season { get; set; }
        public int WinCount { get; set; }
        public int LossCount { get; set; }
        public double WinLossRatio { get; set; }
        public string Finish { get; set; } = string.Empty;
        public double Srs { get; set; }
        public double Pace { get; set; }
        public double RelPace { get; set; }
        public double ORtg { get; set; }
        public double DRtg { get; set; }
        public string Playoffs { get; set; } = string.Empty;
    }
}
