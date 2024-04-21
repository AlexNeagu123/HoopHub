using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.DeleteTeamThread
{
    public class DeleteTeamThreadCommandHandler(ICurrentUserService userService, ITeamThreadRepository teamThreadRepository) : IRequestHandler<DeleteTeamThreadCommand, BaseResponse>
    {
        private readonly ICurrentUserService _currentUserService = userService;
        private readonly ITeamThreadRepository _teamThreadRepository = teamThreadRepository;

        public async Task<BaseResponse> Handle(DeleteTeamThreadCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteTeamThreadCommandValidator(_teamThreadRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BaseResponse.ErrorResponseFromFluentResult(validationResult);

            var threadResult = await _teamThreadRepository.FindByIdAsyncIncludingFan(request.Id);
            if (!threadResult.IsSuccess)
                return BaseResponse.ErrorResponseFromKeyMessage(threadResult.ErrorMsg, ValidationKeys.TeamThread);

            var thread = threadResult.Value;
            var userId = _currentUserService.GetUserId;
            if (userId != thread.FanId)
                return BaseResponse.ErrorResponseFromKeyMessage(ValidationErrors.ThreadDeleteNotAuthorized, ValidationKeys.TeamThread);

            var deleteResult = await _teamThreadRepository.DeleteAsync(thread.Id);
            return !deleteResult.IsSuccess ? BaseResponse.ErrorResponseFromKeyMessage(deleteResult.ErrorMsg, ValidationKeys.TeamThread) : new BaseResponse { Success = true };
        }
    }
}
