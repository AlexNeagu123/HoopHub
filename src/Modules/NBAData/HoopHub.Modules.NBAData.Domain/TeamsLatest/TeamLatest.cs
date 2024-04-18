using System.ComponentModel.DataAnnotations.Schema;
using HoopHub.Modules.NBAData.Domain.Teams;

namespace HoopHub.Modules.NBAData.Domain.TeamsLatest
{
    public class TeamLatest
    {
        [Column("team_id")]
        public Guid TeamId { get; private set; }

        public Team Team { get; private set; }

        [Column("game_date")]
        public string GameDate { get; private set; }

        [Column("latest_index")]
        public int LatestIndex { get; private set; }
    }
}
