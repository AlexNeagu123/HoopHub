using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.GetTeamThreadsPaged
{
    public class GetTeamThreadsPagedQuery : IRequest<PagedResponse<ICollection<TeamThreadDto>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public Guid TeamId { get; set; }
    }
}
