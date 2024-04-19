using HoopHub.BuildingBlocks.Application.Responses;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.DeleteTeamThread
{
    public class DeleteTeamThreadCommand : IRequest<BaseResponse>
    {
        public Guid Id { get; set; }
    }
}
