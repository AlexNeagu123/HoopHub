using HoopHub.API.Hubs;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData;
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
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var response = await LiveScoreGetterService.GetLiveBoxScores(teamRepository, playerRepository, boxScoresDataService);
            await _hubContext.Clients.All.ReceiveLiveBoxScores(response);
            if (!response.Success)
                return;

            foreach (var game in response.Data)
            {
                var createResponse = await mediator.Send(new CreateGameThreadCommand
                {
                    Date = game.Date!,
                    HomeTeamId = game.HomeTeam.Id,
                    VisitorTeamId = game.VisitorTeam.Id
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
