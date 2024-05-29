using System.ComponentModel.DataAnnotations.Schema;
using HoopHub.Modules.NBAData.Domain.Games;
using HoopHub.Modules.NBAData.Domain.Players;
using HoopHub.Modules.NBAData.Domain.Teams;

namespace HoopHub.Modules.NBAData.Domain.AdvancedStatsEntries
{
    public class AdvancedStatsEntry
    {
        [Column("id")]
        public Guid Id { get; private set; }

        [Column("player_id")]
        public Guid PlayerId { get; private set; }

        public Player Player { get; private set; } = null!;

        [Column("team_id")]
        public Guid TeamId { get; private set; }

        public Team Team { get; private set; } = null!;

        [Column("game_id")]
        public Guid GameId { get; private set; }

        public Game Game { get; private set; } = null!;

        [Column("pie")]
        public float? Pie { get; private set; }

        [Column("pace")]
        public float? Pace { get; private set; }

        [Column("assist_percentage")]
        public float? AssistPercentage { get; private set; }

        [Column("assist_ratio")]
        public float? AssistRatio { get; private set; }

        [Column("assist_to_turnover")]
        public float? AssistToTurnover { get; private set; }

        [Column("defensive_rating")]
        public float? DefensiveRating { get; private set; }

        [Column("defensive_rebound_percentage")]
        public float? DefensiveReboundPercentage { get; private set; }

        [Column("effective_field_goal_percentage")]
        public float? EffectiveFieldGoalPercentage { get; private set; }

        [Column("net_rating")]
        public float? NetRating { get; private set; }

        [Column("offensive_rating")]
        public float? OffensiveRating { get; private set; }

        [Column("offensive_rebound_percentage")]
        public float? OffensiveReboundPercentage { get; private set; }

        [Column("rebound_percentage")]
        public float? ReboundPercentage { get; private set; }

        [Column("true_shooting_percentage")]
        public float? TrueShootingPercentage { get; private set; }

        [Column("turnover_ratio")]
        public float? TurnoverRatio { get; private set; }

        [Column("usage_percentage")]
        public float? UsagePercentage { get; private set; }

        private AdvancedStatsEntry(
            Guid playerId,
            Guid teamId,
            Guid gameId,
            float? pie,
            float? pace,
            float? assistPercentage,
            float? assistRatio,
            float? assistToTurnover,
            float? defensiveRating,
            float? defensiveReboundPercentage,
            float? effectiveFieldGoalPercentage,
            float? netRating,
            float? offensiveRating,
            float? offensiveReboundPercentage,
            float? reboundPercentage,
            float? trueShootingPercentage,
            float? turnoverRatio,
            float? usagePercentage)
        {
            Id = Guid.NewGuid();
            PlayerId = playerId;
            TeamId = teamId;
            GameId = gameId;
            Pie = pie;
            Pace = pace;
            AssistPercentage = assistPercentage;
            AssistRatio = assistRatio;
            AssistToTurnover = assistToTurnover;
            DefensiveRating = defensiveRating;
            DefensiveReboundPercentage = defensiveReboundPercentage;
            EffectiveFieldGoalPercentage = effectiveFieldGoalPercentage;
            NetRating = netRating;
            OffensiveRating = offensiveRating;
            OffensiveReboundPercentage = offensiveReboundPercentage;
            ReboundPercentage = reboundPercentage;
            TrueShootingPercentage = trueShootingPercentage;
            TurnoverRatio = turnoverRatio;
            UsagePercentage = usagePercentage;
        }

        public static AdvancedStatsEntry Create(
            Guid playerId,
            Guid teamId,
            Guid gameId,
            float? pie,
            float? pace,
            float? assistPercentage,
            float? assistRatio,
            float? assistToTurnover,
            float? defensiveRating,
            float? defensiveReboundPercentage,
            float? effectiveFieldGoalPercentage,
            float? netRating,
            float? offensiveRating,
            float? offensiveReboundPercentage,
            float? reboundPercentage,
            float? trueShootingPercentage,
            float? turnoverRatio,
            float? usagePercentage)
        {
            return new AdvancedStatsEntry(
                playerId,
                teamId,
                gameId,
                pie,
                pace,
                assistPercentage,
                assistRatio,
                assistToTurnover,
                defensiveRating,
                defensiveReboundPercentage,
                effectiveFieldGoalPercentage,
                netRating,
                offensiveRating,
                offensiveReboundPercentage,
                reboundPercentage,
                trueShootingPercentage,
                turnoverRatio,
                usagePercentage);
        }
    }
}
