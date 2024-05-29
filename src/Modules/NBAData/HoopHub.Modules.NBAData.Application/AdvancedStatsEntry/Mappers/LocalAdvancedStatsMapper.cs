using HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.Dtos;
using HoopHub.Modules.NBAData.Application.Games.Mappers;
using HoopHub.Modules.NBAData.Application.Players.Mappers;
using HoopHub.Modules.NBAData.Application.Teams.Mappers;

namespace HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.Mappers
{
    public class LocalAdvancedStatsMapper
    {
        private readonly LocalGameMapper _gameMapper = new();
        private readonly PlayerMapper _playerMapper = new();
        private readonly TeamMapper _teamMapper = new();

        public LocalStoredAdvancedStatsEntryDto AdvancedStatsToLocalAdvancedStatsEntryDto(
            Domain.AdvancedStatsEntries.AdvancedStatsEntry advancedStats, bool isLicensed)
        {
            return new LocalStoredAdvancedStatsEntryDto
            {
                Id = advancedStats.Id,
                Game = _gameMapper.LocalStoredGameToLocalStoredGameDto(advancedStats.Game, isLicensed),
                Player = _playerMapper.PlayerToPlayerDto(advancedStats.Player, isLicensed),
                Team = _teamMapper.TeamToTeamDto(advancedStats.Team, isLicensed),
                Pie = advancedStats.Pie,
                Pace = advancedStats.Pace,
                AssistPercentage = advancedStats.AssistPercentage,
                AssistRatio = advancedStats.AssistRatio,
                AssistToTurnover = advancedStats.AssistToTurnover,
                DefensiveRating = advancedStats.DefensiveRating,
                DefensiveReboundPercentage = advancedStats.DefensiveReboundPercentage,
                EffectiveFieldGoalPercentage = advancedStats.EffectiveFieldGoalPercentage,
                NetRating = advancedStats.NetRating,
                OffensiveRating = advancedStats.OffensiveRating,
                OffensiveReboundPercentage = advancedStats.OffensiveReboundPercentage,
                ReboundPercentage = advancedStats.ReboundPercentage,
                TrueShootingPercentage = advancedStats.TrueShootingPercentage,
                TurnoverRatio = advancedStats.TurnoverRatio,
                UsagePercentage = advancedStats.UsagePercentage
            };
        }
    }
}
