using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Fans;
using HoopHub.Modules.UserFeatures.Domain.Rules;

namespace HoopHub.Modules.UserFeatures.Domain.Comments
{
    public class CommentVote : AuditableEntity
    {
        public Guid CommentId { get; private set; }
        public ThreadComment ThreadComment { get; private set; } = null!;
        public string FanId { get; private set; }
        public Fan Fan { get; private set; } = null!;
        public bool IsUpVote { get; private set; }

        private CommentVote(Guid commentId, string fanId, bool isUpVote)
        {
            CommentId = commentId;
            FanId = fanId;
            IsUpVote = isUpVote;
        }

        private static Result<CommentVote> Create(Guid commentId, string fanId, bool isUpVote)
        {
            try
            {
                CheckRule(new CommentIdCannotBeEmpty(commentId));
                CheckRule(new FanIdCannotBeEmpty(fanId));
            }
            catch (BusinessRuleValidationException e)
            {
                return Result<CommentVote>.Failure(e.Details);
            }
            return Result<CommentVote>.Success(new CommentVote(commentId, fanId, isUpVote));
        }
        public static Result<CommentVote> UpVote(Guid commentId, string fanId)
        {
            return Create(commentId, fanId, true);
        }

        public static Result<CommentVote> DownVote(Guid commentId, string fanId)
        {
            return Create(commentId, fanId, false);
        }
    }
}
