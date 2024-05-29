using System.ComponentModel.DataAnnotations.Schema;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.NBAData.Domain.AdvancedStatsEntries;
using HoopHub.Modules.NBAData.Domain.Games.Events;
using HoopHub.Modules.NBAData.Domain.Seasons;
using HoopHub.Modules.NBAData.Domain.Teams;

namespace HoopHub.Modules.NBAData.Domain.Games
{
    public class Game : Entity
    {
        [Column("id")]
        public Guid Id { get; private set; }

        [Column("date")]
        public DateTime Date { get; private set; }

        [Column("home_team_id")]
        public Guid HomeTeamId { get; private set; }
        public Team HomeTeam { get; private set; } = null!;

        [Column("visitor_team_id")]
        public Guid VisitorTeamId { get; private set; }
        public Team VisitorTeam { get; private set; } = null!;

        [Column("home_team_score")]
        public int HomeTeamScore { get; private set; }

        [Column("visitor_team_score")]
        public int VisitorTeamScore { get; private set; }

        [Column("status")]
        public string Status { get; private set; }

        [Column("period")]
        public int? Period { get; private set; }

        [Column("time")]
        public string? Time { get; private set; }

        [Column("season_id")]
        public Guid SeasonId { get; private set; }
        public Season Season { get; private set; } = null!;

        [Column("postseason")]
        public bool? Postseason { get; private set; }

        public ICollection<BoxScores.BoxScores> BoxScores { get; private set; } = [];
        public ICollection<AdvancedStatsEntry> AdvancedStatsEntries { get; private set; } = [];
        private Game(DateTime date, Guid homeTeamId, Guid visitorTeamId, int homeTeamScore, int visitorTeamScore, string status, Guid seasonId,
            bool? postseason, string? time, int? period)
        {
            Id = Guid.NewGuid();
            Date = date;
            HomeTeamId = homeTeamId;
            VisitorTeamId = visitorTeamId;
            HomeTeamScore = homeTeamScore;
            VisitorTeamScore = visitorTeamScore;
            Status = status;
            SeasonId = seasonId;
            Postseason = postseason;
            Time = time;
            Period = period;
        }

        public void MarkGameAdded(string homeTeamName, string visitorTeamName, string? homeTeamImageUrl, string? visitorTeamImageUrl, int homeTeamApiId,
            int visitorTeamApiId)
        {
            AddDomainEvent(new GameCreatedDomainEvent(
                Date,
                HomeTeamId,
                VisitorTeamId,
                HomeTeamScore,
                VisitorTeamScore,
                homeTeamName,
                visitorTeamName,
                homeTeamImageUrl,
                visitorTeamImageUrl,
                homeTeamApiId,
                visitorTeamApiId,
                Status,
                SeasonId,
                Postseason,
                Time,
                Period
            ));
        }

        public static Game Create(DateTime date, Guid homeTeamId, Guid visitorTeamId, int homeTeamScore,
            int visitorTeamScore, string status, Guid seasonId, bool postseason, string time, int period)
        {
            return new Game(date, homeTeamId, visitorTeamId, homeTeamScore, visitorTeamScore, status, seasonId, postseason, time, period);
        }
    }
}
