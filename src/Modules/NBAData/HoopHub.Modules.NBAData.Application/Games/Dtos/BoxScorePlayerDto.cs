using HoopHub.Modules.NBAData.Application.Players.Dtos;

namespace HoopHub.Modules.NBAData.Application.Games.Dtos
{
    public class BoxScorePlayerDto
    {
        public string? Min { get; set; }
        public int? Fgm { get; set; }
        public int? Fga { get; set; }
        public double? FgPct { get; set; }
        public int? Fg3m { get; set; }
        public int? Fg3a { get; set; }
        public double? Fg3Pct { get; set; }
        public int? Ftm { get; set; }
        public int? Fta { get; set; }
        public double? FtPct { get; set; }
        public int? Oreb { get; set; }
        public int? Dreb { get; set; }
        public int Reb { get; set; }
        public int? Ast { get; set; }
        public int? Stl { get; set; }
        public int? Blk { get; set; }
        public int? Turnover { get; set; }
        public int? Pf { get; set; }
        public int Pts { get; set; }
        public PlayerDto? Player { get; set; }
    }
}
