using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.CreateGameThread
{
    public class CreateGameThreadCommand : IRequest<Response<GameThreadDto>>
    {
        public Guid HomeTeamId { get; set; }
        public Guid VisitorTeamId { get; set; }
        public string Date { get; set; } = null!;
    }
}
