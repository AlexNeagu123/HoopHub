using System.ComponentModel.DataAnnotations.Schema;
using HoopHub.Modules.NBAData.Domain.Games;
using HoopHub.Modules.NBAData.Domain.Players;
using HoopHub.Modules.NBAData.Domain.Teams;

namespace HoopHub.Modules.NBAData.Domain.BoxScores
{
    public class BoxScores
    {
        [Column("id")]
        public Guid Id { get; private set; }

        [Column("game_id")]
        public Guid GameId { get; private set; }
        public Game Game { get; private set; }

        [Column("player_id")]
        public Guid PlayerId { get; private set; }

        public Player Player { get; private set; }

        [Column("team_id")]
        public Guid TeamId { get; private set; }

        public Team Team { get; private set; }

        [Column("min")]
        public string? Min { get; private set; }

        [Column("fgm")]
        public int Fgm { get; private set; }

        [Column("fga")]
        public int Fga { get; private set; }

        [Column("fg_pct")]
        public double FgPct { get; private set; }

        [Column("fg3m")]
        public int Fg3m { get; private set; }

        [Column("fg3a")]
        public int Fg3a { get; private set; }

        [Column("fg3_pct")]
        public double Fg3Pct { get; private set; }

        [Column("ftm")]
        public int Ftm { get; private set; }

        [Column("fta")]
        public int Fta { get; private set; }

        [Column("ft_pct")]
        public double FtPct { get; private set; }

        [Column("oreb")]
        public int Oreb { get; private set; }

        [Column("dreb")]
        public int Dreb { get; private set; }

        [Column("reb")]
        public int Reb { get; private set; }

        [Column("ast")]
        public int Ast { get; private set; }

        [Column("stl")]
        public int Stl { get; private set; }

        [Column("blk")]
        public int Blk { get; private set; }

        [Column("turnover")]
        public int Turnover { get; private set; }

        [Column("pf")]
        public int Pf { get; private set; }

        [Column("pts")]
        public int Pts { get; private set; }
    }
}
