using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.PlayerFollowEntries.Dtos;
using HoopHub.Modules.UserFeatures.Application.PlayerFollowEntries.Mappers;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.PlayerFollowEntries.GetPlayersFollowed
{
    public class GetPlayersFollowedQueryHandler(
        IPlayerFollowEntryRepository playerFollowEntryRepository,
        ICurrentUserService userService)
        : IRequestHandler<GetPlayersFollowedQuery, Response<IReadOnlyList<PlayerFollowEntryDto>>>
    {
        private readonly IPlayerFollowEntryRepository _playerFollowEntryRepository = playerFollowEntryRepository;
        private readonly ICurrentUserService _userService = userService;
        private readonly PlayerFollowEntryMapper _playerFollowEntryMapper = new();

        public async Task<Response<IReadOnlyList<PlayerFollowEntryDto>>> Handle(GetPlayersFollowedQuery request, CancellationToken cancellationToken)
        {
            var fanId = _userService.GetUserId;
            if(request.FanId != null)
                fanId = request.FanId;

            var playerFollowEntryResult = await _playerFollowEntryRepository.GetAllByFanIdIncludingFanAsync(fanId!);
            if (!playerFollowEntryResult.IsSuccess)
                return Response<IReadOnlyList<PlayerFollowEntryDto>>.ErrorResponseFromKeyMessage(playerFollowEntryResult.ErrorMsg, ValidationKeys.PlayerFollowEntry);

            var playerFollowEntries = playerFollowEntryResult.Value;
            var playerFollowEntryDtoList = playerFollowEntries.Select(p => _playerFollowEntryMapper.PlayerFollowEntryToPlayerFollowEntryDto(p)).ToList();

            return new Response<IReadOnlyList<PlayerFollowEntryDto>>
            {
                Success = true,
                Data = playerFollowEntryDtoList
            };
        }
    }
}
