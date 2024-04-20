using HoopHub.Modules.UserFeatures.Application.Comments.Dtos;
using HoopHub.Modules.UserFeatures.Application.Fans.Mappers;
using HoopHub.Modules.UserFeatures.Domain.Comments;

namespace HoopHub.Modules.UserFeatures.Application.Comments.Mappers
{
    public class ThreadCommentVoteMapper
    {
        private readonly FanMapper _fanMapper = new();
        private readonly ThreadCommentMapper _commentMapper = new();

        public ThreadCommentVoteDto CommentVoteToCommentVoteDto(CommentVote threadCommentVote)
        {
            return new ThreadCommentVoteDto
            {
                Fan = _fanMapper.FanToFanDto(threadCommentVote.Fan),
                ThreadComment = _commentMapper.ThreadCommentToThreadCommentDto(threadCommentVote.ThreadComment),
                IsUpvote = threadCommentVote.IsUpVote
            };
        }
    }
}
