using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Fans.Dtos;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Fans.GetFanProfile
{
    public class GetFanProfileQuery : IRequest<Response<FanDto>>
    {
        public string? FanId { get; set; } = null;
    }
}
