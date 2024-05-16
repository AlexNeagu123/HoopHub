using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;
using HoopHub.Modules.UserFeatures.Domain.Threads;

namespace HoopHub.Modules.UserFeatures.Application.Threads.Mappers
{
    public class GameThreadMapper
    {
        public GameThreadDto GameThreadToGameThreadDto(GameThread gameThread)
        {
            return new GameThreadDto
            {
                Id = gameThread.Id,
                HomeTeamId = gameThread.HomeTeamApiId,
                VisitorTeamId = gameThread.VisitorTeamApiId,
                Date = gameThread.Date,
                CommentsCount = gameThread.CommentsCount
            };
        }
    }
}
