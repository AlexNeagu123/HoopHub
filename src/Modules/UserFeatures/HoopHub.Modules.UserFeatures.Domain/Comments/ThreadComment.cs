using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using HoopHub.Modules.UserFeatures.Domain.Fans;
using HoopHub.Modules.UserFeatures.Domain.Threads;

namespace HoopHub.Modules.UserFeatures.Domain.Comments
{
    public class ThreadComment : AuditableEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public Guid? ParentId { get; private set; }
        public string Content { get; private set; }
        public Guid? TeamThreadId { get; private set; }
        public TeamThread? TeamThread { get; private set; }

        public Guid? GameThreadId { get; private set; }
        public GameThread? GameThread { get; private set; }

        public string FanId { get; private set; }
        public Fan Fan { get; private set; } = null!;
        public int UpVotes { get; private set; }
        public int DownVotes { get; private set; }
        public ICollection<CommentVote> Votes { get; private set; } = [];


        private ThreadComment(string content, string fanId)
        {
            Content = content;
            FanId = fanId;
        }

        public static Result<ThreadComment> Create(string content, string fanId)
        {
            if (string.IsNullOrWhiteSpace(content))
                return Result<ThreadComment>.Failure(ValidationErrors.InvalidContent);

            return Result<ThreadComment>.Success(new ThreadComment(content, fanId));
        }

        public void UpVote()
        {
            UpVotes++;
        }

        public void DownVote()
        {
            DownVotes++;
        }

        public void AttachParentId(Guid parentId)
        {
            ParentId = parentId;
        }

        public void AttachTeamThread(Guid teamThreadId)
        {
            TeamThreadId = teamThreadId;
        }

        public void AttachGameThread(Guid gameThreadId)
        {
            GameThreadId = gameThreadId;
        }
    }
}
