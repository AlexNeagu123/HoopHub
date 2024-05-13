using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Comments;

namespace HoopHub.Modules.UserFeatures.Domain.Threads
{
    public abstract class BaseThread : AuditableEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public ICollection<ThreadComment> Comments { get; private set; } = [];
        public int CommentsCount { get; private set; }

        public void UpdateCommentCount(int delta)
        {
            CommentsCount += delta;
        }
    }
}
