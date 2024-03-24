namespace HoopHub.Modules.NBAData.Application.Games.Dtos
{
    public class GameWithBoxScoreDto
    {
        public string? Date { get; set; }
        public int Season { get; set; }
        public int Period { get; set; }
        public string? Status { get; set; }
        public string? Time { get; set; }
        public bool Postseason { get; set; }
        public int HomeTeamScore { get; set; }
        public int VisitorTeamScore { get; set; }
        public BoxScoreTeamDto HomeTeam { get; set; }
        public BoxScoreTeamDto VisitorTeam { get; set; }
    }
}
