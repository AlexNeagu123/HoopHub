using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.Dtos;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.CreateAdvancedStatsEntry
{
    public class CreateAdvancedStatsEntryCommand : IRequest<Response<LocalStoredAdvancedStatsEntryDto>>
    {
        public DateTime Date { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public int VisitorTeamId { get; set; }
        public int HomeTeamId { get; set; }
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
