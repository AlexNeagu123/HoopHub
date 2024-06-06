namespace HoopHub.Modules.NBAData.Application.GamePredictions.Models
{
    public class AverageTeamStatistics
    {
        public int Streak { get; set; }
        public float WinPercentage { get; set; }
        public float WinPercentageLast5 { get; set; }
        public float Points { get; set; }
        public float PointsLast5 { get; set; }
        public float Assists { get; set; }
        public float AssistsLast5 { get; set; }
        public float Rebounds { get; set; }
        public float ReboundsLast5 { get; set; }
        public float Steals { get; set; }
        public float StealsLast5 { get; set; }
        public float Blocks { get; set; }
        public float BlocksLast5 { get; set; }
        public float Turnovers { get; set; }
        public float TurnoversLast5 { get; set; }
    }
}
