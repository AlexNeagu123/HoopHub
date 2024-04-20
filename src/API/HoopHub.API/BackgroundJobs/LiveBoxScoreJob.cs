using HoopHub.API.Hubs;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData;
using HoopHub.Modules.NBAData.Application.Persistence;
using Microsoft.AspNetCore.SignalR;
using Quartz;

namespace HoopHub.API.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class LiveBoxScoreJob(IServiceScopeFactory serviceScopeFactory, IHubContext<LiveBoxScoreHub, ILiveBoxScoreClient> hubContext) : IJob
    {
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
        private readonly IHubContext<LiveBoxScoreHub, ILiveBoxScoreClient> _hubContext = hubContext;

        public async Task Execute(IJobExecutionContext context)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var teamRepository = scope.ServiceProvider.GetRequiredService<ITeamRepository>();
            var playerRepository = scope.ServiceProvider.GetRequiredService<IPlayerRepository>();
            var boxScoresDataService = scope.ServiceProvider.GetRequiredService<IBoxScoresDataService>();

            var response = await LiveScoreGetterService.GetLiveBoxScores(teamRepository, playerRepository, boxScoresDataService);
            await _hubContext.Clients.All.ReceiveLiveBoxScores(response);
        }
    }
}
