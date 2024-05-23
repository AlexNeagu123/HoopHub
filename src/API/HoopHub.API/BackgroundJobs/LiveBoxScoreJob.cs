using HoopHub.API.Hubs;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.GamesData;
using HoopHub.Modules.NBAData.Application.Games.CreateGame;
using HoopHub.Modules.NBAData.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Threads.CreateGameThread;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Quartz;

namespace HoopHub.API.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class LiveBoxScoreJob(IServiceScopeFactory serviceScopeFactory, IHubContext<LiveBoxScoreHub, ILiveBoxScoreClient> hubContext, ILogger<LiveBoxScoreJob> logger) : IJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
        private readonly IHubContext<LiveBoxScoreHub, ILiveBoxScoreClient> _hubContext = hubContext;
        private readonly ILogger<LiveBoxScoreJob> _logger = logger;

        public async Task Execute(IJobExecutionContext context)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var teamRepository = scope.ServiceProvider.GetRequiredService<ITeamRepository>();
            var playerRepository = scope.ServiceProvider.GetRequiredService<IPlayerRepository>();

            var boxScoresDataService = scope.ServiceProvider.GetRequiredService<IBoxScoresDataService>();
            var gamesDataService = scope.ServiceProvider.GetRequiredService<IGamesDataService>();
            var gamesRepository = scope.ServiceProvider.GetRequiredService<IGameRepository>();
            var boxScoresRepository = scope.ServiceProvider.GetRequiredService<IBoxScoresRepository>();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            await HandleLiveBoxScores(teamRepository, playerRepository, boxScoresDataService, mediator);
            await HandleNewFinishedGames(gamesRepository, boxScoresRepository, boxScoresDataService, gamesDataService, mediator);
        }

        private async Task HandleNewFinishedGames(IGameRepository gamesRepository, IBoxScoresRepository boxScoresRepository, IBoxScoresDataService boxScoresDataService, IGamesDataService gamesDataService, IMediator mediator)
        {
            var date = DateTime.Today;
            var yesterday = date.AddDays(-1).ToUniversalTime();
            var gamesResult = await gamesRepository.FindGamesByDate(yesterday);
            var yesterdaysGames = gamesResult.Value;

            if (yesterdaysGames.Count != 0)
            {
                _logger.LogInformation($"Yesterdays games already inserted into the database");
                return;
            }

            var response = await gamesDataService.GetGamesByDateAsync(yesterday.ToString("yyyy-MM-dd"));
            if (!response.IsSuccess)
            {
                _logger.LogError($"Error getting games for {yesterday}");
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
            var response = await LiveScoreGetterService.GetLiveBoxScores(teamRepository, playerRepository, boxScoresDataService);
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
