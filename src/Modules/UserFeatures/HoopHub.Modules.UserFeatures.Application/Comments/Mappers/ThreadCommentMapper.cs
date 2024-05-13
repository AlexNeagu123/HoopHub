using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Application.Comments.Dtos;
using HoopHub.Modules.UserFeatures.Application.Fans.Mappers;
using HoopHub.Modules.UserFeatures.Application.Threads.Mappers;
using HoopHub.Modules.UserFeatures.Domain.Comments;

namespace HoopHub.Modules.UserFeatures.Application.Comments.Mappers
{
    public class ThreadCommentMapper
    {
        private readonly FanMapper _fanMapper = new();
        private readonly TeamThreadMapper _teamThreadMapper = new();
        private readonly GameThreadMapper _gameThreadMapper = new();

        public ThreadCommentDto ThreadCommentToThreadCommentDto(ThreadComment comment, VoteStatus? voteStatus = null)
        {
            return new ThreadCommentDto
            {
                Id = comment.Id,
                ParentId = comment.ParentId,
                Content = comment.Content,
                Fan = comment.Fan != null! ? _fanMapper.FanToFanDto(comment.Fan) : null,
                TeamThread = comment.TeamThread != null ? _teamThreadMapper.TeamThreadToTeamThreadDto(comment.TeamThread) : null,
                GameThread = comment.GameThread != null ? _gameThreadMapper.GameThreadToGameThreadDto(comment.GameThread) : null,
                UpVotes = comment.UpVotes,
                DownVotes = comment.DownVotes,
                CreatedDate = comment.CreatedDate,
                VoteStatus = voteStatus,
                RepliesCount = comment.RepliesCount,
                RespondsToFan = comment.RespondsTo != null ? _fanMapper.FanToFanDto(comment.RespondsTo) : null
            };
        }
    }
}
