using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Application.Fans.Dtos;
using HoopHub.Modules.UserFeatures.Application.Threads.Dtos;

namespace HoopHub.Modules.UserFeatures.Application.Comments.Dtos
{
    public class ThreadCommentDto
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Content { get; set; } = string.Empty;
        public TeamThreadDto? TeamThread { get; set; }
        public GameThreadDto? GameThread { get; set; }
        public FanDto? Fan { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public DateTime CreatedDate { get; set; }
        public VoteStatus? VoteStatus { get; set; }
    }
}
