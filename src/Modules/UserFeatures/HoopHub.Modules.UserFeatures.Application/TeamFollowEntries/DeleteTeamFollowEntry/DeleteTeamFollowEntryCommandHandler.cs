using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.TeamFollowEntries.DeleteTeamFollowEntry
{
    public class DeleteTeamFollowEntryCommandHandler(
        ITeamFollowEntryRepository teamFollowEntryRepository,
        ICurrentUserService userService)
        : IRequestHandler<DeleteTeamFollowEntryCommand, BaseResponse>
    {
        private readonly ITeamFollowEntryRepository _teamFollowEntryRepository = teamFollowEntryRepository;
        private readonly ICurrentUserService _userService = userService;

        public async Task<BaseResponse> Handle(DeleteTeamFollowEntryCommand request,
            CancellationToken cancellationToken)
        {
            var fanId = _userService.GetUserId;
            var validator = new DeleteTeamFollowEntryCommandValidator(_teamFollowEntryRepository, fanId!);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BaseResponse.ErrorResponseFromFluentResult(validationResult);

            var teamFollowEntryResult = await _teamFollowEntryRepository.FindByIdPairIncludingFanAsync(fanId!, request.TeamId);
            if (!teamFollowEntryResult.IsSuccess)
                return BaseResponse.ErrorResponseFromKeyMessage(teamFollowEntryResult.ErrorMsg, ValidationKeys.TeamFollowEntry);

            var teamFollowEntry = teamFollowEntryResult.Value;
            var deleteTeamFollowEntryResult = await _teamFollowEntryRepository.RemoveAsync(teamFollowEntry);

            return deleteTeamFollowEntryResult.IsSuccess
                ? new BaseResponse { Success = true }
                : BaseResponse.ErrorResponseFromKeyMessage(deleteTeamFollowEntryResult.ErrorMsg, ValidationKeys.TeamFollowEntry);
        }
    }
}
