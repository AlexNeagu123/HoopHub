using HoopHub.Modules.NBAData.Domain.Teams;
using System.ComponentModel.DataAnnotations.Schema;
using HoopHub.Modules.NBAData.Domain.Seasons;

namespace HoopHub.Modules.NBAData.Domain.TeamBios
{
    public class TeamBio
    {
        [Column("id")]
        public Guid Id { get; private set; }

        [Column("team_id")]
        public Guid TeamId { get; private set; }
        public Team Team { get; private set; }

        [Column("season_id")]
        public Guid SeasonId { get; private set; }
        public Season Season { get; private set; }

        [Column("win_count")]
        public int WinCount { get; private set; }

        [Column("loss_count")]
        public int LossCount { get; private set; }

        [Column("win_loss_ratio")]
        public double WinLossRatio { get; private set; }

        [Column("finish")]
        public string Finish { get; private set; }

        [Column("srs")]
        public double Srs { get; private set; }

        [Column("pace")]
        public double Pace { get; private set; }

        [Column("rel_pace")]
        public double RelPace { get; private set; }

        [Column("o_rtg")]
        public double ORtg { get; private set; }

        [Column("d_rtg")]
        public double DRtg { get; private set; }

        [Column("playoffs")]
        public string Playoffs { get; private set; }
    }
}
