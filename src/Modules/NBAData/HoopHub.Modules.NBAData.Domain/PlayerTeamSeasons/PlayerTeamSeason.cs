using System.ComponentModel.DataAnnotations.Schema;
using HoopHub.Modules.NBAData.Domain.Players;
using HoopHub.Modules.NBAData.Domain.Seasons;
using HoopHub.Modules.NBAData.Domain.Teams;

namespace HoopHub.Modules.NBAData.Domain.PlayerTeamSeasons
{
    public class PlayerTeamSeason
    {
        [Column("player_id")]
        public Guid PlayerId { get; private set; }
        public Player Player { get; private set; }

        [Column("team_id")]
        public Guid TeamId { get; private set; }
        public Team Team { get; private set; }

        [Column("season_id")]
        public Guid SeasonId { get; private set; }
        public Season Season { get; private set; }
    }
}
