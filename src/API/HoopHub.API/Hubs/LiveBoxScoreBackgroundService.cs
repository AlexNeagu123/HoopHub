// Define a background service

using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData;
using HoopHub.Modules.NBAData.Application.Games.BoxScores;
using HoopHub.Modules.NBAData.Application.Games.Dtos;
using HoopHub.Modules.NBAData.Application.Persistence;
using Microsoft.AspNetCore.SignalR;

namespace HoopHub.API.Hubs;

public class LiveBoxScoreBackgroundService(IServiceScopeFactory serviceScopeFactory, IHubContext<LiveBoxScoreHub, ILiveBoxScoreClient> hubContext)
    : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
    private readonly IHubContext<LiveBoxScoreHub, ILiveBoxScoreClient> _hubContext = hubContext;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var interval = TimeSpan.FromSeconds(30);
        var timer = new PeriodicTimer(interval);
        while (!stoppingToken.IsCancellationRequested)
        {
            await SendLiveBoxScores();
            await timer.WaitForNextTickAsync(stoppingToken);
        }
    }

    private async Task SendLiveBoxScores()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        
        var teamRepository = scope.ServiceProvider.GetRequiredService<ITeamRepository>();
        var playerRepository = scope.ServiceProvider.GetRequiredService<IPlayerRepository>();
        var boxScoresDataService = scope.ServiceProvider.GetRequiredService<IBoxScoresDataService>();

        var boxScores = await boxScoresDataService.GetLiveBoxScores();
        if (!boxScores.IsSuccess)
        {
            var failureResponse = new Response<IReadOnlyList<GameWithBoxScoreDto>>
            {
                Success = false,
                Data = null!
            };
            await _hubContext.Clients.All.ReceiveLiveBoxScores(failureResponse);
        }

        var boxScoreProcessor = new BoxScoreProcessor(teamRepository, playerRepository);
        var liveBoxScores = boxScores.Value;

        List<GameWithBoxScoreDto> liveProcessedBoxScores = [];

        foreach (var liveBoxScore in liveBoxScores)
        {
            var boxScore = await boxScoreProcessor.ProcessApiBoxScoreAndConvert(liveBoxScore);
            liveProcessedBoxScores.Add(boxScore.Data);
        }


        var successResponse = new Response<IReadOnlyList<GameWithBoxScoreDto>>
        {
            Success = true,
            Data = liveProcessedBoxScores
        };

        await _hubContext.Clients.All.ReceiveLiveBoxScores(successResponse);
    }
}