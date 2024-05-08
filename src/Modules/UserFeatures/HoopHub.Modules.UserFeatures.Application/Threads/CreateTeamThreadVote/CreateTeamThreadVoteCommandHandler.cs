using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;
using HoopHub.Modules.UserFeatures.Application.Threads.Mappers;
using HoopHub.Modules.UserFeatures.Domain.Threads;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.CreateTeamThreadVote
{
    public class CreateTeamThreadVoteCommandHandler(ITeamThreadRepository teamThreadRepository, ITeamThreadVoteRepository teamThreadVoteRepository,
        ICurrentUserService userService) : IRequestHandler<CreateTeamThreadVoteCommand, Response<TeamThreadVoteDto>>
    {
        private readonly ITeamThreadRepository _teamThreadRepository = teamThreadRepository;
        private readonly ITeamThreadVoteRepository _teamThreadVoteRepository = teamThreadVoteRepository;
        private readonly ICurrentUserService _userService = userService;
        private readonly TeamThreadVoteMapper _teamThreadVoteMapper = new();
        public async Task<Response<TeamThreadVoteDto>> Handle(CreateTeamThreadVoteCommand request, CancellationToken cancellationToken)
        {
            var fanId = _userService.GetUserId!;
            var validator = new CreateTeamThreadVoteCommandValidator(_teamThreadRepository, _teamThreadVoteRepository, fanId);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<TeamThreadVoteDto>.ErrorResponseFromFluentResult(validationResult);

            var teamThreadVoteResult = TeamThreadVote.Create(request.ThreadId, fanId, request.IsUpvote);
            if (!teamThreadVoteResult.IsSuccess)
                return Response<TeamThreadVoteDto>.ErrorResponseFromKeyMessage(teamThreadVoteResult.ErrorMsg, ValidationKeys.TeamThreadVote);

            var teamThreadVote = teamThreadVoteResult.Value;
            teamThreadVote.MarkAsAdded();

            var addTeamThreadVoteResult = await _teamThreadVoteRepository.AddAsync(teamThreadVote);
            if (!addTeamThreadVoteResult.IsSuccess)
                return Response<TeamThreadVoteDto>.ErrorResponseFromKeyMessage(addTeamThreadVoteResult.ErrorMsg, ValidationKeys.TeamThreadVote);


            var addedTeamThreadVote = await _teamThreadVoteRepository.FindByIdAsyncIncludingAll(teamThreadVote.TeamThreadId, teamThreadVote.FanId);
            return new Response<TeamThreadVoteDto>
            {
                Success = true,
                Data = _teamThreadVoteMapper.TeamThreadVoteToTeamThreadVoteDto(addedTeamThreadVote.Value)
            };
        }
    }
}
