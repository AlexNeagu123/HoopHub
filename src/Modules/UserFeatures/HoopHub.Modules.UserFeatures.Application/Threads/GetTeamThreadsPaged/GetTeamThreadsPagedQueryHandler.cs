using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;
using HoopHub.Modules.UserFeatures.Application.Threads.Mappers;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Threads.GetTeamThreadsPaged
{
    public class GetTeamThreadsPagedQueryHandler(ITeamThreadRepository teamThreadRepository, ICurrentUserService userService, ITeamThreadVoteRepository teamThreadVoteRepository) : IRequestHandler<GetTeamThreadsPagedQuery, PagedResponse<ICollection<TeamThreadDto>>>
    {
        private readonly ITeamThreadRepository _teamThreadRepository = teamThreadRepository;
        private readonly ICurrentUserService _currentUserService = userService;
        private readonly ITeamThreadVoteRepository _teamThreadVoteRepository = teamThreadVoteRepository;
        private readonly TeamThreadMapper _teamThreadMapper = new();
        public async Task<PagedResponse<ICollection<TeamThreadDto>>> Handle(GetTeamThreadsPagedQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetTeamThreadsPagedQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return PagedResponse<ICollection<TeamThreadDto>>.ErrorResponseFromFluentResult(validationResult);

            var fanId = _currentUserService.GetUserId;
            var threadsResult = request.AreOwn ? await _teamThreadRepository.GetByTeamIdAndFanIdPagedAsync(request.TeamId, fanId!, request.Page, request.PageSize)
                : await _teamThreadRepository.GetByTeamIdPagedAsync(request.TeamId, request.Page, request.PageSize);

            if (!threadsResult.IsSuccess)
                return PagedResponse<ICollection<TeamThreadDto>>.ErrorResponseFromKeyMessage(threadsResult.ErrorMsg, ValidationKeys.TeamThread);

            var threads = threadsResult.Value;
            var threadVoteStatuses = new List<ThreadVoteStatus>();

            foreach (var thread in threads)
            {
                var commentVote = await _teamThreadVoteRepository.FindByIdAsyncIncludingAll(thread.Id, fanId);
                var status = !commentVote.IsSuccess ? ThreadVoteStatus.None :
                    commentVote.Value.IsUpVote ? ThreadVoteStatus.UpVoted : ThreadVoteStatus.DownVoted;
                threadVoteStatuses.Add(status);
            }

            var threadsDto = threads.Select((thread, index) => _teamThreadMapper
                .TeamThreadToTeamThreadDto(thread, threadVoteStatuses[index])).ToList();


            return new PagedResponse<ICollection<TeamThreadDto>>
            {
                Success = true,
                Data = threadsDto,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalRecords = threadsResult.TotalCount,
            };
        }
    }
}
