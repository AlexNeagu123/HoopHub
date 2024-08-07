﻿using HoopHub.BuildingBlocks.Application.Responses;
using HoopHub.BuildingBlocks.Application.Services;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Application.Comments.Dtos;
using HoopHub.Modules.UserFeatures.Application.Comments.Mappers;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Comments;
using MediatR;

namespace HoopHub.Modules.UserFeatures.Application.Comments.GetCommentsPagedByThread
{
    public class GetCommentsPagedByThreadQueryHandler(
        IThreadCommentRepository threadCommentRepository, ITeamThreadRepository teamThreadRepository,
        IThreadCommentVoteRepository threadCommentVoteRepository,
        ICurrentUserService currentUserService)
        : IRequestHandler<GetCommentsPagedByThreadQuery, PagedResponse<IReadOnlyList<ThreadCommentDto>>>
    {
        private readonly IThreadCommentRepository _threadCommentRepository = threadCommentRepository;
        private readonly ITeamThreadRepository _teamThreadRepository = teamThreadRepository;
        private readonly IThreadCommentVoteRepository _threadCommentVoteRepository = threadCommentVoteRepository;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly ThreadCommentMapper _threadCommentMapper = new();

        public async Task<PagedResponse<IReadOnlyList<ThreadCommentDto>>> Handle(GetCommentsPagedByThreadQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetCommentsPagedByThreadQueryValidator(_threadCommentRepository, _teamThreadRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return PagedResponse<IReadOnlyList<ThreadCommentDto>>.ErrorResponseFromFluentResult(validationResult);

            PagedResult<IReadOnlyList<ThreadComment>> commentsResult = null!;
            if (request.TeamThreadId.HasValue)
            {
                var teamThreadId = request.TeamThreadId.Value;
                commentsResult = request.IsPopular
                    ? await _threadCommentRepository.GetPagedByTeamThreadMostPopularAsync(teamThreadId, request.Page, request.PageSize)
                    : await _threadCommentRepository.GetPagedByTeamThreadAsync(teamThreadId, request.Page, request.PageSize);
            }
            else if (request.GameThreadId.HasValue)
            {
                var gameThreadId = request.GameThreadId.Value;
                commentsResult = request.IsPopular
                    ? await _threadCommentRepository.GetPagedByGameThreadMostPopularAsync(gameThreadId, request.Page, request.PageSize)
                    : await _threadCommentRepository.GetPagedByGameThreadAsync(gameThreadId, request.Page, request.PageSize);
            }

            if (!commentsResult.IsSuccess)
                return PagedResponse<IReadOnlyList<ThreadCommentDto>>.ErrorResponseFromKeyMessage(commentsResult.ErrorMsg, ValidationKeys.ThreadComment);

            var comments = commentsResult.Value.ToList();
            if (request.FirstComment.HasValue)
            {
                var firstCommentResult = await _threadCommentRepository.FindByIdAsyncIncludingAll(request.FirstComment.Value);
                if (!firstCommentResult.IsSuccess)
                    return PagedResponse<IReadOnlyList<ThreadCommentDto>>.ErrorResponseFromKeyMessage(firstCommentResult.ErrorMsg, ValidationKeys.ThreadComment);

                if (comments.All(c => c.Id != firstCommentResult.Value.Id))
                {
                    comments = comments.Take(comments.Count - 1).ToList();
                }
                else
                {
                    var removeIndex = comments.TakeWhile(comment => comment.Id != firstCommentResult.Value.Id).Count();
                    comments.RemoveAt(removeIndex);
                }
                comments.Insert(0, firstCommentResult.Value);
            }

            var commentVoteStatuses = new List<VoteStatus>();
            var fanId =_currentUserService.GetUserId!;
            
            foreach(var comment in comments)
            {
                var commentVote = await _threadCommentVoteRepository.FindByIdAsyncIncludingAll(comment.Id, fanId);
                var status = !commentVote.IsSuccess ? VoteStatus.None :
                    commentVote.Value.IsUpVote ? VoteStatus.UpVoted : VoteStatus.DownVoted;
                commentVoteStatuses.Add(status);
            }
            
            var commentsDto = comments.Select((x, index) => _threadCommentMapper.ThreadCommentToThreadCommentDto(x, commentVoteStatuses[index])).ToList();
            return new PagedResponse<IReadOnlyList<ThreadCommentDto>>
            {
                Success = true,
                Data = commentsDto,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalRecords = commentsResult.TotalCount,
            };
        }
    }
}
