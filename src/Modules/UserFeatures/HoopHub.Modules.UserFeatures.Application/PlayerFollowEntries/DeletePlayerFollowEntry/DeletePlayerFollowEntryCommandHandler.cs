using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.PlayerFollowEntries.DeletePlayerFollowEntry
{
    public class DeletePlayerFollowEntryCommandHandler(
        IPlayerFollowEntryRepository playerFollowEntryRepository,
        ICurrentUserService userService)
        : IRequestHandler<DeletePlayerFollowEntryCommand, BaseResponse>
    {
        private readonly IPlayerFollowEntryRepository _playerFollowEntryRepository = playerFollowEntryRepository;
        private readonly ICurrentUserService _userService = userService;

        public async Task<BaseResponse> Handle(DeletePlayerFollowEntryCommand request, CancellationToken cancellationToken)
        {
            var fanId = _userService.GetUserId;
            var validator = new DeletePlayerFollowEntryCommandValidator(_playerFollowEntryRepository, fanId!);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BaseResponse.ErrorResponseFromFluentResult(validationResult);

            var playerFollowEntryResult = await _playerFollowEntryRepository.FindByIdPairIncludingFanAsync(fanId!, request.PlayerId);
            if (!playerFollowEntryResult.IsSuccess)
                return BaseResponse.ErrorResponseFromKeyMessage(playerFollowEntryResult.ErrorMsg, ValidationKeys.PlayerFollowEntry);

            var playerFollowEntry = playerFollowEntryResult.Value;
            var deletePlayerFollowEntryResult = await _playerFollowEntryRepository.RemoveAsync(playerFollowEntry);

            return deletePlayerFollowEntryResult.IsSuccess
                ? new BaseResponse { Success = true }
                : BaseResponse.ErrorResponseFromKeyMessage(deletePlayerFollowEntryResult.ErrorMsg, ValidationKeys.PlayerFollowEntry);
        }
    }
}
