using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.GetGameThreadsByDate
{
    public class GetGameThreadsByDateQuery : IRequest<Response<IReadOnlyList<GameThreadDto>>>
    {
        public string Date { get; set; } = null!;
    }
}
