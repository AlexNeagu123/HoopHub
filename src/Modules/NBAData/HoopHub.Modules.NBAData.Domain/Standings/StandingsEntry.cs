using System.ComponentModel.DataAnnotations.Schema;
using HoopHub.Modules.NBAData.Domain.Seasons;
using HoopHub.Modules.NBAData.Domain.Teams;

namespace HoopHub.Modules.NBAData.Domain.Standings
{
    public class StandingsEntry
    {
        [Column("season_id")]
        public Guid SeasonId { get; private set; }
        public Season Season { get; private set; }

        [Column("team_id")]
        public Guid TeamId { get; private set; }
        public Team Team { get; private set; }


        [Column("rank")]
        public int Rank { get; private set; }

        [Column("overall")]
        public string Overall { get; private set; } = string.Empty;

        [Column("home")]
        public string Home { get; private set; } = string.Empty;

        [Column("road")]
        public string Road { get; private set; } = string.Empty;

        [Column("eastern_record")]
        public string EasternRecord { get; private set; } = string.Empty;

        [Column("western_record")]
        public string WesternRecord { get; private set; } = string.Empty;
    }
}
