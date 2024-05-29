using HoopHub.API.Hubs;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.NBAData.Application.AdvancedStatsEntry.CreateAdvancedStatsEntry;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.AdvancedStatsData;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.GamesData;
using HoopHub.Modules.NBAData.Application.Games.BoxScores.CreateBoxScore;
using HoopHub.Modules.NBAData.Application.Games.CreateGame;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Threads.CreateGameThread;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Quartz;

namespace HoopHub.API.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class LiveBoxScoreJob(IServiceScopeFactory serviceScopeFactory, IHubContext<LiveBoxScoreHub, ILiveBoxScoreClient> hubContext, ILogger<LiveBoxScoreJob> logger, ICurrentUserService currentUserService) : IJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
        private readonly IHubContext<LiveBoxScoreHub, ILiveBoxScoreClient> _hubContext = hubContext;
        private readonly ILogger<LiveBoxScoreJob> _logger = logger;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task Execute(IJobExecutionContext context)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var teamRepository = scope.ServiceProvider.GetRequiredService<ITeamRepository>();
            var playerRepository = scope.ServiceProvider.GetRequiredService<IPlayerRepository>();

            var boxScoresDataService = scope.ServiceProvider.GetRequiredService<IBoxScoresDataService>();
            var gamesDataService = scope.ServiceProvider.GetRequiredService<IGamesDataService>();
            var gamesRepository = scope.ServiceProvider.GetRequiredService<IGameRepository>();
            var boxScoresRepository = scope.ServiceProvider.GetRequiredService<IBoxScoresRepository>();
            var advancedStatsRepository = scope.ServiceProvider.GetRequiredService<IAdvancedStatsEntryRepository>();
            var advancedStatsDataService = scope.ServiceProvider.GetRequiredService<IAdvancedStatsDataService>();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await HandleLiveBoxScores(teamRepository, playerRepository, boxScoresDataService, mediator);
            await HandleNewFinishedGames(gamesRepository, boxScoresRepository, boxScoresDataService, gamesDataService, mediator);
            await HandleNewBoxScores(boxScoresRepository, boxScoresDataService, mediator);
            await HandleNewAdvancedStats(advancedStatsRepository, advancedStatsDataService, mediator);
        }

        private async Task HandleNewAdvancedStats(IAdvancedStatsEntryRepository advancedStatsRepository, IAdvancedStatsDataService advancedStatsDataService, IMediator mediator)
        {
            var date = DateTime.Today;
            var yesterday = date.AddDays(-1).ToUniversalTime();
            var advancedStatsResult = await advancedStatsRepository.GetByDateAsync(yesterday);
            var advancedStats = advancedStatsResult.Value;

            if (advancedStats.Count != 0)
            {
                _logger.LogInformation("Yesterday's advanced stats already inserted into the database");
                return;
            }

            var externalAdvancedStatsResult = await advancedStatsDataService.GetAdvancedStatsAsyncByDate(yesterday.ToString("yyyy-MM-dd"));
            if (!externalAdvancedStatsResult.IsSuccess)
            {
                _logger.LogError($"Error getting external advanced stats for {yesterday}");
                _logger.LogError($"{externalAdvancedStatsResult.ErrorMsg}");
                return;
            }

            var externalAdvancedStats = externalAdvancedStatsResult.Value;

            foreach (var stat in externalAdvancedStats)
            {
                var statDate = DateTime.Parse(stat.Game.Date).ToUniversalTime();
                await HandlePlayersAdvancedStats(stat.Player, stat.Player.TeamId, stat, statDate, mediator);
            }
        }

        private async Task HandlePlayersAdvancedStats(AdvancedStatsPlayerApiDto player, int teamId, AdvancedStatsApiDto stat, DateTime statDate, IMediator mediator)
        {
            var command = new CreateAdvancedStatsEntryCommand
            {
                Date = statDate,
                PlayerId = player.Id,
                TeamId = teamId,
                HomeTeamId = stat.Game.HomeTeamId,
                Pie = stat.Pie,
                VisitorTeamId = stat.Game.VisitorTeamId,
                Pace = stat.Pace,
                AssistPercentage = stat.AssistPercentage,
                AssistRatio = stat.AssistRatio,
                AssistToTurnover = stat.AssistToTurnover,
                DefensiveRating = stat.DefensiveRating,
                DefensiveReboundPercentage = stat.DefensiveReboundPercentage,
                EffectiveFieldGoalPercentage = stat.EffectiveFieldGoalPercentage,
                NetRating = stat.NetRating,
                OffensiveRating = stat.OffensiveRating,
                OffensiveReboundPercentage = stat.OffensiveReboundPercentage,
                ReboundPercentage = stat.ReboundPercentage,
                TrueShootingPercentage = stat.TrueShootingPercentage,
                TurnoverRatio = stat.TurnoverRatio,
                UsagePercentage = stat.UsagePercentage
            };

            var result = await mediator.Send(command);

            if (!result.Success)
            {
                _logger.LogError($"Failed to save advanced stats for player {player.Id} on {statDate}");
                foreach (var error in result.ValidationErrors)
                {
                    _logger.LogError($"{error.Key}: {error.Value}");
                }
            }
        }

        private async Task HandleNewBoxScores(IBoxScoresRepository boxScoresRepository, IBoxScoresDataService boxScoresDataService, ISender mediator)
        {
            var date = DateTime.Today;
            var yesterday = date.AddDays(-1).ToUniversalTime();
            var boxScoresResult = await boxScoresRepository.GetByDateAsync(yesterday);
            var boxScores = boxScoresResult.Value;

            if (boxScores.Count != 0)
            {
                _logger.LogInformation($"Yesterdays box-scores already inserted into the database");
                return;
            }

            var externalBoxScoresResult = await boxScoresDataService.GetBoxScoresAsyncByDate(yesterday.ToString("yyyy-MM-dd"));
            if (!externalBoxScoresResult.IsSuccess)
            {
                _logger.LogError($"Error getting external box-scores for {yesterday}");
                return;
            }

            var externalBoxScores = externalBoxScoresResult.Value;
            foreach (var boxScore in externalBoxScores)
            {
                var boxScoreDate = DateTime.Parse(boxScore.Date).ToUniversalTime();
                await HandlePlayersBoxScores(boxScore.HomeTeam.Players, boxScore.HomeTeam.Id, boxScore, boxScoreDate, mediator);
                await HandlePlayersBoxScores(boxScore.VisitorTeam.Players, boxScore.VisitorTeam.Id, boxScore, boxScoreDate, mediator);
            }
        }

        private async Task HandlePlayersBoxScores(List<BoxScorePlayerApiDto> teamPlayers, int teamId, BoxScoreApiDto boxScore, DateTime boxScoreDate, ISender mediator)
        {
            foreach (var player in teamPlayers)
            {
                var mediatorResponse = await mediator.Send(new CreateBoxScoreCommand
                {
                    Date = boxScoreDate,
                    PlayerId = player.Player.Id,
                    TeamId = teamId,
                    VisitorTeamId = boxScore.VisitorTeam.Id,
                    HomeTeamId = boxScore.HomeTeam.Id,
                    Min = player.Min,
                    Fgm = player.Fgm,
                    Fga = player.Fga,
                    FgPct = player.FgPct,
                    Fg3m = player.Fg3m,
                    Fg3a = player.Fg3a,
                    Fg3Pct = player.Fg3Pct,
                    Ftm = player.Ftm,
                    Fta = player.Fta,
                    FtPct = player.FtPct,
                    Oreb = player.Oreb,
                    Dreb = player.Dreb,
                    Reb = player.Reb,
                    Ast = player.Ast,
                    Stl = player.Stl,
                    Blk = player.Blk,
                    Turnover = player.Turnover,
                    Pf = player.Pf,
                    Pts = player.Pts
                });

                if (!mediatorResponse.Success)
                {
                    _logger.LogInformation($"Box score for player {player.Player.Id} on {boxScoreDate} has already been created");
                }
            }
        }

        private async Task HandleNewFinishedGames(IGameRepository gamesRepository, IBoxScoresRepository boxScoresRepository, IBoxScoresDataService boxScoresDataService, IGamesDataService gamesDataService, ISender mediator)
        {
            var date = DateTime.Today;
            var yesterday = date.AddDays(-1).ToUniversalTime();
            var gamesResult = await gamesRepository.FindGamesByDate(yesterday);
            var yesterdaysGames = gamesResult.Value;

            if (yesterdaysGames.Count != 0)
            {
                _logger.LogInformation("Yesterdays games already inserted into the database");
                return;
            }

            var response = await gamesDataService.GetGamesByDateAsync(yesterday.ToString("yyyy-MM-dd"));
            if (!response.IsSuccess)
            {
                _logger.LogError("Error getting games for {yesterday}");
                return;
            }

            foreach (var game in response.Value)
            {
                var createResponse = await mediator.Send(new CreateGameCommand
                {
                    Date = DateTime.Parse(game.Date).ToUniversalTime(),
                    HomeTeamApiId = game.HomeTeam.Id,
                    VisitorTeamApiId = game.VisitorTeam.Id,
                    HomeTeamScore = game.HomeTeamScore,
                    VisitorTeamScore = game.VisitorTeamScore,
                    Status = game.Status,
                    SeasonPeriod = $"{game.Season}-{(game.Season + 1).ToString()[2..]}",
                    Postseason = game.Postseason,
                    Time = game.Time,
                    Period = game.Period
                });

                if (!createResponse.Success)
                {
                    _logger.LogInformation($"Game for game {game.HomeTeam.Id} vs {game.VisitorTeam.Id} on {game.Date} has already been created");
                    continue;
                }

                _logger.LogInformation($"Game for game {game.HomeTeam.Id} vs {game.VisitorTeam.Id} on {game.Date} created successfully");
            }

        }


        public async Task HandleLiveBoxScores(ITeamRepository teamRepository, IPlayerRepository playerRepository, IBoxScoresDataService boxScoresDataService,
            IMediator mediator)
        {
            var isLicensed = _currentUserService.GetUserLicense ?? false;
            var response = await LiveScoreGetterService.GetLiveBoxScores(teamRepository, playerRepository, boxScoresDataService, isLicensed);
            await _hubContext.Clients.All.ReceiveLiveBoxScores(response);
            if (!response.Success)
                return;

            foreach (var game in response.Data)
            {
                var createResponse = await mediator.Send(new CreateGameThreadCommand
                {
                    Date = game.Date!,
                    HomeTeamApiId = game.HomeTeam.ApiId,
                    VisitorTeamApiId = game.VisitorTeam.ApiId
                });

                if (!createResponse.Success)
                {
                    _logger.LogInformation($"Game thread for game {game.HomeTeam.Id} vs {game.VisitorTeam.Id} on {game.Date} has already been created");
                    continue;
                }
                _logger.LogInformation($"Game thread for game {game.HomeTeam.Id} vs {game.VisitorTeam.Id} on {game.Date} created successfully");
            }
        }
    }
}
