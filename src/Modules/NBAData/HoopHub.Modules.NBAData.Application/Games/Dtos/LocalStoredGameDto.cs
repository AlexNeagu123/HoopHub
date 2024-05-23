using HoopHub.Modules.NBAData.Application.Seasons.Dtos;
using HoopHub.Modules.NBAData.Application.Teams.Dtos;

namespace HoopHub.Modules.NBAData.Application.Games.Dtos
{
    public class LocalStoredGameDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TeamDto? HomeTeam { get; set; }
        public TeamDto? VisitorTeam { get; set; }
        public SeasonDto? Season { get; set; }
        public int HomeTeamScore { get; set; }
        public int VisitorTeamScore { get; set; }
        public string Status { get; set; } = string.Empty;
        public int? Period { get; set; }
        public string? Time { get; set; } = string.Empty;
        public bool? Postseason { get; set; }
    }
}
