using HoopHub.Modules.NBAData.Application.Games.Dtos;
using HoopHub.Modules.NBAData.Application.Players.Dtos;
using HoopHub.Modules.NBAData.Application.Teams.Dtos;

namespace HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.Dtos
{
    public class LocalStoredAdvancedStatsEntryDto
    {
        public Guid Id { get; set; }
        public PlayerDto Player { get; set; }
        public TeamDto Team { get; set; }
        public LocalStoredGameDto Game { get; set; }
        public float? Pie { get; set; }
        public float? Pace { get; set; }
        public float? AssistPercentage { get; set; }
        public float? AssistRatio { get; set; }
        public float? AssistToTurnover { get; set; }
        public float? DefensiveRating { get; set; }
        public float? DefensiveReboundPercentage { get; set; }
        public float? EffectiveFieldGoalPercentage { get; set; }
        public float? NetRating { get; set; }
        public float? OffensiveRating { get; set; }
        public float? OffensiveReboundPercentage { get; set; }
        public float? ReboundPercentage { get; set; }
        public float? TrueShootingPercentage { get; set; }
        public float? TurnoverRatio { get; set; }
        public float? UsagePercentage { get; set; }
    }
}
