using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Fans;
using HoopHub.Modules.UserFeatures.Domain.Rules;
using HoopHub.Modules.UserFeatures.Domain.Threads;

namespace HoopHub.Modules.UserFeatures.Domain.Comments
{
    public class ThreadComment : AuditableEntity, ISoftDeletable
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

        public bool IsDeleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }

        private ThreadComment(string content, string fanId)
        {
            Content = content;
            FanId = fanId;
            IsDeleted = false;
        }

        public static Result<ThreadComment> Create(string content, string fanId)
        {
            try
            {
                CheckRule(new CommentContentMustBeValid(content));
                CheckRule(new FanIdCannotBeEmpty(fanId));
            }
            catch (BusinessRuleValidationException e)
            {
                return Result<ThreadComment>.Failure(e.Details);
            }
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
            try
            {
                CheckRule(new ParentCommentIdCannotBeEmpty(parentId));
                ParentId = parentId;
            } catch(BusinessRuleValidationException) { }
        }

        public void AttachTeamThread(Guid teamThreadId)
        {
            try
            {
                CheckRule(new ThreadIdCannotBeEmpty(teamThreadId));
                TeamThreadId = teamThreadId;
            }
            catch (BusinessRuleValidationException) {}
        }

        public void AttachGameThread(Guid gameThreadId)
        {
            try
            {
                CheckRule(new ThreadIdCannotBeEmpty(gameThreadId));
                GameThreadId = gameThreadId;
            }
            catch (BusinessRuleValidationException) { }
        }
    }
}
