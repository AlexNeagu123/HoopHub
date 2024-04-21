using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.PlayerFollowEntries.Dtos;
using HoopHub.Modules.UserFeatures.Application.PlayerFollowEntries.Mappers;
using HoopHub.Modules.UserFeatures.Domain.Follows;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.PlayerFollowEntries.CreatePlayerFollowEntry
{
    public class CreatePlayerFollowEntryCommandHandler(IPlayerFollowEntryRepository playerFollowEntryRepository, ICurrentUserService userService) : IRequestHandler<CreatePlayerFollowEntryCommand, Response<PlayerFollowEntryDto>>
    {
        private readonly IPlayerFollowEntryRepository _playerFollowEntryRepository = playerFollowEntryRepository;
        private readonly ICurrentUserService _userService = userService;
        private readonly PlayerFollowEntryMapper _playerFollowEntryMapper = new();

        public async Task<Response<PlayerFollowEntryDto>> Handle(CreatePlayerFollowEntryCommand request, CancellationToken cancellationToken)
        {
            var fanId = _userService.GetUserId;
            var validator = new CreatePlayerFollowEntryCommandValidator(_playerFollowEntryRepository, fanId!);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<PlayerFollowEntryDto>.ErrorResponseFromFluentResult(validationResult);

            var playerFollowEntryResult = PlayerFollowEntry.Create(fanId!, request.PlayerId);
            if (!playerFollowEntryResult.IsSuccess)
                return Response<PlayerFollowEntryDto>.ErrorResponseFromKeyMessage(playerFollowEntryResult.ErrorMsg, ValidationKeys.PlayerFollowEntry);

            var playerFollowEntry = playerFollowEntryResult.Value;
            var addedEntryResult = await _playerFollowEntryRepository.AddAsync(playerFollowEntry);
            if (!addedEntryResult.IsSuccess)
                return Response<PlayerFollowEntryDto>.ErrorResponseFromKeyMessage(addedEntryResult.ErrorMsg, ValidationKeys.PlayerFollowEntry);

            var completeEntryResult = await _playerFollowEntryRepository.FindByIdPairIncludingFanAsync(fanId!, request.PlayerId);
            return new Response<PlayerFollowEntryDto>
            {
                Success = true,
                Data = _playerFollowEntryMapper.PlayerFollowEntryToPlayerFollowEntryDto(completeEntryResult.Value)
            };
        }
    }
}

