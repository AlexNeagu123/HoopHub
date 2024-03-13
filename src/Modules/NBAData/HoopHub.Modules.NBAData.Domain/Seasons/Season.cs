using System.ComponentModel.DataAnnotations.Schema;
using HoopHub.Modules.NBAData.Domain.PlayerTeamSeasons;

namespace HoopHub.Modules.NBAData.Domain.Seasons
{
    public class Season
    {
        [Column("id")]
        public Guid Id { get; private set; }

        [Column("season")]
        public string SeasonPeriod { get; private set; } = string.Empty;

        public ICollection<PlayerTeamSeason> PlayerTeamSeasons { get; private set; }
    }
}
