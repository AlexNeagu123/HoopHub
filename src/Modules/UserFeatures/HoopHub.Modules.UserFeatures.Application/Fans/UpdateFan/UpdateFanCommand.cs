using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.Modules.UserFeatures.Application.Fans.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HoopHub.Modules.UserFeatures.Application.Fans.UpdateFan
{
    public class UpdateFanCommand : IRequest<Response<FanDto>>
    {
        public IFormFile? ProfileImage { get; set; }
        public Guid? FavouriteTeamId { get; set; }
    }
}
