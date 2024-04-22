using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Fans.Dtos;
using HoopHub.Modules.UserFeatures.Application.Fans.Mappers;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Fans.GetFanProfile
{
    public class GetFanProfileQueryHandler(IFanRepository fanRepository, ICurrentUserService userService)
        : IRequestHandler<GetFanProfileQuery, Response<FanDto>>
    {
        private readonly IFanRepository _fanRepository = fanRepository;
        private readonly ICurrentUserService _userService = userService;
        private readonly FanMapper _fanMapper = new();

        public async Task<Response<FanDto>> Handle(GetFanProfileQuery request, CancellationToken cancellationToken)
        {
            var fanId = request.FanId ?? _userService.GetUserId;
            var fanResult = await _fanRepository.FindByIdAsync(fanId!);
            if (!fanResult.IsSuccess)
                return Response<FanDto>.ErrorResponseFromKeyMessage(fanResult.ErrorMsg, ValidationKeys.Fan);

            return new Response<FanDto>
            {
                Success = true,
                Data = _fanMapper.FanToFanDto(fanResult.Value)
            };
        }
    }
}
