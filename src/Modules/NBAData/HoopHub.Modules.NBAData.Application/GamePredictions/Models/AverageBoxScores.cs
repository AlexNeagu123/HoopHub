namespace HoopHub.Modules.NBAData.Application.GamePredictions.Models
{
    public class AverageBoxScores
    {
        public int PlayerId { get; set; }
        public float Min { get; set; } = 0;
        public float Fgm { get; set; } = 0;
        public float Fga { get; set; } = 0;
        public float FgPct { get; set; } = 0;
        public float Fg3m { get; set; } = 0;
        public float Fg3a { get; set; } = 0;
        public float Fg3Pct { get; set; } = 0;
        public float Ftm { get; set; } = 0;
        public float Fta { get; set; } = 0;
        public float FtPct { get; set; } = 0;
        public float Oreb { get; set; } = 0;
        public float Dreb { get; set; } = 0;
        public float Reb { get; set; } = 0;
        public float Ast { get; set; } = 0;
        public float Stl { get; set; } = 0;
        public float Blk { get; set; } = 0;
        public float Turnover { get; set; } = 0;
        public float Pf { get; set; } = 0;
        public float Pts { get; set; } = 0;
    }
}
