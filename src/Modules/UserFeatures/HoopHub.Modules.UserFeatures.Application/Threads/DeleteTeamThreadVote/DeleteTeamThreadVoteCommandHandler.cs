using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.DeleteTeamThreadVote
{
    public class DeleteTeamThreadVoteCommandHandler(
        ITeamThreadRepository teamThreadRepository,
        ITeamThreadVoteRepository teamThreadVoteRepository,
        ICurrentUserService userService)
        : IRequestHandler<DeleteTeamThreadVoteCommand, BaseResponse>
    {
        private readonly ITeamThreadVoteRepository _teamThreadVoteRepository = teamThreadVoteRepository;
        private readonly ICurrentUserService _userService = userService;

        public async Task<BaseResponse> Handle(DeleteTeamThreadVoteCommand request, CancellationToken cancellationToken)
        {
            var fanId = _userService.GetUserId!;
            var validator = new DeleteTeamThreadVoteCommandValidator(_teamThreadVoteRepository, fanId);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BaseResponse.ErrorResponseFromFluentResult(validationResult);

            var teamThreadVoteResult = await _teamThreadVoteRepository.FindByIdAsyncIncludingAll(request.ThreadId, fanId);
            if (!teamThreadVoteResult.IsSuccess)
                return BaseResponse.ErrorResponseFromKeyMessage(teamThreadVoteResult.ErrorMsg, ValidationKeys.TeamThreadVote);

            var teamThreadVote = teamThreadVoteResult.Value;
            teamThreadVote.MarkAsDeleted();

            var deleteTeamThreadVoteResult = await _teamThreadVoteRepository.RemoveAsync(teamThreadVote);
            return !deleteTeamThreadVoteResult.IsSuccess ? BaseResponse.ErrorResponseFromKeyMessage(deleteTeamThreadVoteResult.ErrorMsg, ValidationKeys.TeamThreadVote) 
                : new BaseResponse { Success = true };
        }
    }
}
