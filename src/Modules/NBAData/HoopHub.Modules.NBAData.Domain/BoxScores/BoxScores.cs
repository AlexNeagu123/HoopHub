using System.ComponentModel.DataAnnotations.Schema;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.NBAData.Domain.BoxScores.Events;
using HoopHub.Modules.NBAData.Domain.Games;
using HoopHub.Modules.NBAData.Domain.Players;
using HoopHub.Modules.NBAData.Domain.Teams;

namespace HoopHub.Modules.NBAData.Domain.BoxScores
{
    public class BoxScores : Entity
    {
        [Column("id")]
        public Guid Id { get; private set; }

        [Column("game_id")]
        public Guid GameId { get; private set; }

        public Game Game { get; private set; } = null!;

        [Column("player_id")]
        public Guid PlayerId { get; private set; }

        public Player Player { get; private set; } = null!;

        [Column("team_id")]
        public Guid TeamId { get; private set; }

        public Team Team { get; private set; } = null!;

        [Column("min")]
        public string? Min { get; private set; }

        [Column("fgm")]
        public int? Fgm { get; private set; }

        [Column("fga")]
        public int? Fga { get; private set; }

        [Column("fg_pct")]
        public double? FgPct { get; private set; }

        [Column("fg3m")]
        public int? Fg3m { get; private set; }

        [Column("fg3a")]
        public int? Fg3a { get; private set; }

        [Column("fg3_pct")]
        public double? Fg3Pct { get; private set; }

        [Column("ftm")]
        public int? Ftm { get; private set; }

        [Column("fta")]
        public int? Fta { get; private set; }

        [Column("ft_pct")]
        public double? FtPct { get; private set; }

        [Column("oreb")]
        public int? Oreb { get; private set; }

        [Column("dreb")]
        public int? Dreb { get; private set; }

        [Column("reb")]
        public int? Reb { get; private set; }

        [Column("ast")]
        public int? Ast { get; private set; }

        [Column("stl")]
        public int? Stl { get; private set; }

        [Column("blk")]
        public int? Blk { get; private set; }

        [Column("turnover")]
        public int? Turnover { get; private set; }

        [Column("pf")]
        public int? Pf { get; private set; }

        [Column("pts")]
        public int? Pts { get; private set; }

        private BoxScores(
            Guid gameId,
            Guid playerId,
            Guid teamId,
            string? min,
            int? fgm,
            int? fga,
            double? fgPct,
            int? fg3m,
            int? fg3a,
            double? fg3Pct,
            int? ftm,
            int? fta,
            double? ftPct,
            int? oreb,
            int? dreb,
            int? reb,
            int? ast,
            int? stl,
            int? blk,
            int? turnover,
            int? pf,
            int? pts)
        {
            Id = Guid.NewGuid();
            GameId = gameId;
            PlayerId = playerId;
            TeamId = teamId;
            Min = min;
            Fgm = fgm;
            Fga = fga;
            FgPct = fgPct;
            Fg3m = fg3m;
            Fg3a = fg3a;
            Fg3Pct = fg3Pct;
            Ftm = ftm;
            Fta = fta;
            FtPct = ftPct;
            Oreb = oreb;
            Dreb = dreb;
            Reb = reb;
            Ast = ast;
            Stl = stl;
            Blk = blk;
            Turnover = turnover;
            Pf = pf;
            Pts = pts;
        }

        public static BoxScores Create(
            Guid gameId,
            Guid playerId,
            Guid teamId,
            string? min,
            int? fgm,
            int? fga,
            double? fgPct,
            int? fg3m,
            int? fg3a,
            double? fg3Pct,
            int? ftm,
            int? fta,
            double? ftPct,
            int? oreb,
            int? dreb,
            int? reb,
            int? ast,
            int? stl,
            int? blk,
            int? turnover,
            int? pf,
            int? pts)
        {
            return new BoxScores(
                gameId,
                playerId,
                teamId,
                min,
                fgm,
                fga,
                fgPct,
                fg3m,
                fg3a,
                fg3Pct,
                ftm,
                fta,
                ftPct,
                oreb,
                dreb,
                reb,
                ast,
                stl,
                blk,
                turnover,
                pf,
                pts);
        }

        public void MarkAsAdded(int homeTeamApiId, int visitorTeamApiId, string playerName, DateTime date, string? playerImageUrl)
        {
            AddDomainEvent(new BoxScoresCreatedDomainEvent(
                GameId,
                PlayerId,
                TeamId,
                homeTeamApiId,
                visitorTeamApiId,
                playerName,
                date,
                playerImageUrl,
                Min,
                Fgm,
                Fga,
                FgPct,
                Fg3m,
                Fg3a,
                Fg3Pct,
                Ftm,
                Fta,
                FtPct,
                Oreb,
                Dreb,
                Reb,
                Ast,
                Stl,
                Blk,
                Turnover,
                Pf,
                Pts));
        }
    }
}
