using Microsoft.AspNetCore.SignalR;

namespace HoopHub.API.Hubs
{
    public sealed class LiveBoxScoreHub : Hub<ILiveBoxScoreClient>
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.ReceiveMessage("Hello from the background service");
        }
    }
}
