using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.GetTeamThreadsByUserPaged
{
    public class GetTeamThreadsByUserPagedQuery : IRequest<PagedResponse<ICollection<TeamThreadDto>>>
    {
        public string? FanId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
