using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.NBAData.Application.Constants;
using MediatR;

namespace HoopHub.Modules.NBAData.Application.Players.GetBioByPlayerId
{
    public class GetBioByPlayerIdQuery(Guid playerId, int startSeason, int endSeason) : IRequest<Response<PlayerDto>>
    {
        public Guid PlayerId { get; set; } = playerId;
        public int StartSeason { get; set; } = startSeason != 0 ? startSeason : Config.CurrentSeason;
        public int EndSeason { get; set; } = endSeason != 0 ? endSeason : Config.CurrentSeason + 1;
    }
}
