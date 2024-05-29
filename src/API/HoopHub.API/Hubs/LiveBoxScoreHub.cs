using HoopHub.API.BackgroundJobs;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.NBAData.Application.ExternalApiServices.BoxScoresData;
using HoopHub.Modules.NBAData.Application.Persistence;
using Microsoft.AspNetCore.SignalR;

namespace HoopHub.API.Hubs
{
    public sealed class LiveBoxScoreHub(ITeamRepository teamRepository, IPlayerRepository playerRepository, IBoxScoresDataService boxScoresDataService, ICurrentUserService currentUserService) : Hub<ILiveBoxScoreClient>
    {
        private readonly ITeamRepository _teamRepository = teamRepository;
        private readonly IPlayerRepository _playerRepository = playerRepository;
        private readonly IBoxScoresDataService _boxScoresDataService = boxScoresDataService;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public override async Task OnConnectedAsync()
        {
            var isLicensed = _currentUserService.GetUserLicense ?? false;
            await Clients.Client(Context.ConnectionId).ReceiveMessage("Connection with the socket done successfully..");
            var response = await LiveScoreGetterService.GetLiveBoxScores(_teamRepository, _playerRepository, _boxScoresDataService, isLicensed);
            await Clients.Client(Context.ConnectionId).ReceiveLiveBoxScores(response);
        }
    }
}
