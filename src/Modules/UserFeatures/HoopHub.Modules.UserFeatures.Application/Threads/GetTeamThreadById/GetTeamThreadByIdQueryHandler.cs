using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;
using HoopHub.Modules.UserFeatures.Application.Threads.Mappers;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.GetTeamThreadById
{
    public class GetTeamThreadByIdQueryHandler(ITeamThreadRepository teamThreadRepository, 
        ITeamThreadVoteRepository teamThreadVoteRepository, ICurrentUserService userService)
        : IRequestHandler<GetTeamThreadByIdQuery, Response<TeamThreadDto>>
    {
        private readonly ITeamThreadRepository _teamThreadRepository = teamThreadRepository;
        private readonly ITeamThreadVoteRepository _teamThreadVoteRepository = teamThreadVoteRepository;
        private readonly ICurrentUserService _userService = userService;
        private readonly TeamThreadMapper _teamThreadMapper = new();
        public async Task<Response<TeamThreadDto>> Handle(GetTeamThreadByIdQuery request, CancellationToken cancellationToken)
        {
            var fanId = _userService.GetUserId!;
            var validator = new GetTeamThreadByIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<TeamThreadDto>.ErrorResponseFromFluentResult(validationResult);

            var threadResult = await _teamThreadRepository.FindByIdAsyncIncludingFan(request.TeamThreadId);
            if (!threadResult.IsSuccess)
                return Response<TeamThreadDto>.ErrorResponseFromKeyMessage(threadResult.ErrorMsg, ValidationKeys.TeamThread);

            var thread = threadResult.Value;
            var commentVote = await _teamThreadVoteRepository.FindByIdAsyncIncludingAll(thread.Id, fanId);

            var status = !commentVote.IsSuccess ? VoteStatus.None :
                commentVote.Value.IsUpVote ? VoteStatus.UpVoted : VoteStatus.DownVoted;

            var threadDto = _teamThreadMapper.TeamThreadToTeamThreadDto(thread, status);
            return new Response<TeamThreadDto>
            {
                Success = true,
                Data = threadDto
            };
        }
    }
}
