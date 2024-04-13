using System.ComponentModel.DataAnnotations.Schema;
using HoopHub.Modules.NBAData.Domain.Seasons;
using HoopHub.Modules.NBAData.Domain.Teams;

namespace HoopHub.Modules.NBAData.Domain.Standings
{
    public class PlayoffSeries
    {
        [Column("season_id")]
        public Guid SeasonId { get; private set; }
        public Season Season { get; private set; }

        [Column("winning_team_id")]
        public Guid WinningTeamId { get; private set; }
        public Team WinningTeam { get; private set; }

        [Column("losing_team_id")]
        public Guid LosingTeamId { get; private set; }
        public Team LosingTeam { get; private set; }

        [Column("winning_team_wins")]
        public int WinningTeamWins { get; private set; }

        [Column("losing_team_wins")]
        public int LosingTeamWins { get; private set; }

        [Column("stage")]
        public string Stage { get; private set; }

        [Column("winning_team_rank")]
        public int WinningTeamRank { get; private set; }

        [Column("losing_team_rank")]
        public int LosingTeamRank { get; private set; }
    }
}
