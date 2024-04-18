using HoopHub.Modules.NBAData.Domain.Players;
using System.ComponentModel.DataAnnotations.Schema;
using HoopHub.Modules.NBAData.Domain.PlayerTeamSeasons;
using HoopHub.Modules.NBAData.Domain.Standings;
using HoopHub.Modules.NBAData.Domain.TeamBios;
using HoopHub.Modules.NBAData.Domain.TeamsLatest;

namespace HoopHub.Modules.NBAData.Domain.Teams
{
    public class Team
    {
        [Column("id")]
        public Guid Id { get; private set; }

        [Column("api_id")]
        public int ApiId { get; private set; }

        [Column("full_name")]
        public string FullName { get; private set; }

        [Column("abbreviation")]
        public string Abbreviation { get; private set; }

        [Column("city")]
        public string? City { get; private set; }

        [Column("conference")]
        public string? Conference { get; private set; }

        [Column("division")]
        public string? Division { get; private set; }

        [Column("image_url")]
        public string? ImageUrl { get; private set; }

        [Column("is_active")]
        public bool IsActive { get; private set; }

        public ICollection<Player> Players { get; set; }
        public ICollection<PlayerTeamSeason> PlayerTeamSeasons { get; private set; }
        public ICollection<TeamBio> TeamBio { get; private set; }
        public ICollection<StandingsEntry> Standings { get; private set; }
        public ICollection<PlayoffSeries> PlayoffSeries { get; private set; }
        public ICollection<TeamLatest> TeamLatest { get; private set; }
    }
}
