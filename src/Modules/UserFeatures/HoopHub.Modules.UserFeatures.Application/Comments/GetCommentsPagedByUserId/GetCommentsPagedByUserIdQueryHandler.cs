using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Application.Comments.Dtos;
using HoopHub.Modules.UserFeatures.Application.Comments.Mappers;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Comments.GetCommentsPagedByUserId
{
    public class GetCommentsPagedByUserIdQueryHandler(
        IThreadCommentRepository threadCommentRepository,
        IThreadCommentVoteRepository threadCommentVoteRepository,
        ICurrentUserService currentUserService)
        : IRequestHandler<GetCommentsPagedByUserIdQuery, PagedResponse<IReadOnlyList<ThreadCommentDto>>>
    {
        private readonly IThreadCommentRepository _threadCommentRepository = threadCommentRepository;
        private readonly IThreadCommentVoteRepository _threadCommentVoteRepository = threadCommentVoteRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly ThreadCommentMapper _threadCommentMapper = new();

        public async Task<PagedResponse<IReadOnlyList<ThreadCommentDto>>> Handle(GetCommentsPagedByUserIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetCommentsPagedByUserIdQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return PagedResponse<IReadOnlyList<ThreadCommentDto>>.ErrorResponseFromFluentResult(validationResult);

            var commentResult = request.IsPopular
                ? await _threadCommentRepository.GetPagedByFanMostPopularAsync(request.UserId, request.Page, request.PageSize)
                : await _threadCommentRepository.GetPagedByFanAsync(request.UserId, request.Page, request.PageSize);

            if (!commentResult.IsSuccess)
                return PagedResponse<IReadOnlyList<ThreadCommentDto>>.ErrorResponseFromKeyMessage(commentResult.ErrorMsg, ValidationKeys.ThreadComment);

            var comments = commentResult.Value.ToList();
            var requesterId = _currentUserService.GetUserId!;

            var commentVoteStatuses = new List<VoteStatus>();
            foreach (var vote in comments)
            {
                var commentVote = await _threadCommentVoteRepository.FindByIdAsyncIncludingAll(vote.Id, requesterId);
                var status = !commentVote.IsSuccess ? VoteStatus.None :
                    commentVote.Value.IsUpVote ? VoteStatus.UpVoted : VoteStatus.DownVoted;
                commentVoteStatuses.Add(status);
            }

            var commentsDto = comments.Select((comment, index) => _threadCommentMapper.ThreadCommentToThreadCommentDto(comment, commentVoteStatuses[index])).ToList();
            return new PagedResponse<IReadOnlyList<ThreadCommentDto>>
            {
                Success = true,
                Data = commentsDto,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalRecords = commentResult.TotalCount
            };
        }
    }
}
