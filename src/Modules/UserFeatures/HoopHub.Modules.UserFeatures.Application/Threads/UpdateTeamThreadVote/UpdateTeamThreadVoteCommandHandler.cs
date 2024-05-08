using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;
using HoopHub.Modules.UserFeatures.Application.Threads.Mappers;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.UpdateTeamThreadVote
{
    public class UpdateTeamThreadVoteCommandHandler(
        ITeamThreadRepository teamThreadRepository,
        ITeamThreadVoteRepository teamThreadVoteRepository,
        ICurrentUserService userService)
        : IRequestHandler<UpdateTeamThreadVoteCommand, Response<TeamThreadVoteDto>>
    {
        private readonly ITeamThreadVoteRepository _teamThreadVoteRepository = teamThreadVoteRepository;
        private readonly ICurrentUserService _userService = userService;
        private readonly TeamThreadVoteMapper _teamThreadVoteMapper = new();

        public async Task<Response<TeamThreadVoteDto>> Handle(UpdateTeamThreadVoteCommand request, CancellationToken cancellationToken)
        {
            var fanId = _userService.GetUserId!;
            var validator = new UpdateTeamThreadVoteCommandValidator(_teamThreadVoteRepository, fanId);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<TeamThreadVoteDto>.ErrorResponseFromFluentResult(validationResult);

            var teamThreadVoteResult = await _teamThreadVoteRepository.FindByIdAsyncIncludingAll(request.ThreadId, fanId);
            if (!teamThreadVoteResult.IsSuccess)
                return Response<TeamThreadVoteDto>.ErrorResponseFromKeyMessage(teamThreadVoteResult.ErrorMsg, ValidationKeys.TeamThreadVote);

            var teamThreadVote = teamThreadVoteResult.Value;
            teamThreadVote.Update(request.IsUpvote);
            var updateTeamThreadVoteResult = await _teamThreadVoteRepository.UpdateAsync(teamThreadVote);

            return updateTeamThreadVoteResult.IsSuccess
                ? new Response<TeamThreadVoteDto> { Success = true, Data = _teamThreadVoteMapper.TeamThreadVoteToTeamThreadVoteDto(teamThreadVote) }
                : Response<TeamThreadVoteDto>.ErrorResponseFromKeyMessage(updateTeamThreadVoteResult.ErrorMsg, ValidationKeys.TeamThreadVote);
        }
    }
}
