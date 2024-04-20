using HoopHub.Modules.UserFeatures.Application.Fans.Dtos;

namespace HoopHub.Modules.UserFeatures.Application.Comments.Dtos
{
    public class ThreadCommentVoteDto
    {
        public FanDto Fan { get; set; }
        public ThreadCommentDto ThreadComment { get; set; }
        public bool IsUpvote { get; set; }
    }
}
