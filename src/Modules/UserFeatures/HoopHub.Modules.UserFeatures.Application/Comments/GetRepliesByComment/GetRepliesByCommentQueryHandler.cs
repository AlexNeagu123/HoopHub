using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Application.Comments.Dtos;
using HoopHub.Modules.UserFeatures.Application.Comments.Mappers;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Comments.GetRepliesByComment
{
    public class GetRepliesByCommentQueryHandler(IThreadCommentRepository threadCommentRepository, ICurrentUserService currentUserService, IThreadCommentVoteRepository threadCommentVoteRepository)
        : IRequestHandler<GetRepliesByCommentQuery, Response<IReadOnlyList<ThreadCommentDto>>>
    {
        private readonly IThreadCommentRepository _threadCommentRepository = threadCommentRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IThreadCommentVoteRepository _threadCommentVoteRepository = threadCommentVoteRepository;
        private readonly ThreadCommentMapper _threadCommentMapper = new();

        public async Task<Response<IReadOnlyList<ThreadCommentDto>>> Handle(GetRepliesByCommentQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetRepliesByCommentQueryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return Response<IReadOnlyList<ThreadCommentDto>>.ErrorResponseFromFluentResult(validationResult);

            var repliesResult = await _threadCommentRepository.GetRepliesByComment(request.CommentId);
            var replies = repliesResult.Value;
            var fanId = _currentUserService.GetUserId!;

            var repliesStatuses = new List<VoteStatus>();
            foreach (var reply in replies)
            {
                var commentVote = await _threadCommentVoteRepository.FindByIdAsyncIncludingAll(reply.Id, fanId);
                var status = !commentVote.IsSuccess ? VoteStatus.None :
                    commentVote.Value.IsUpVote ? VoteStatus.UpVoted : VoteStatus.DownVoted;
                repliesStatuses.Add(status);
            }

            var repliesDtoList = replies.Select((r, index) => _threadCommentMapper.ThreadCommentToThreadCommentDto(r, repliesStatuses[index])).ToList();

            return new Response<IReadOnlyList<ThreadCommentDto>>
            {
                Data = repliesDtoList,
                Success = true
            };
        }
    }
}
