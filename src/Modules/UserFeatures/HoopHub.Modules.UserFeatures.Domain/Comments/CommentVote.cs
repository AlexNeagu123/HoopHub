using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Comments.Events;
using HoopHub.Modules.UserFeatures.Domain.Fans;
using HoopHub.Modules.UserFeatures.Domain.Rules;

namespace HoopHub.Modules.UserFeatures.Domain.Comments
{
    public class CommentVote : AuditableEntity, ISoftDeletable
    {
        public Guid CommentId { get; private set; }
        public ThreadComment ThreadComment { get; private set; } = null!;
        public string FanId { get; private set; }
        public Fan Fan { get; private set; } = null!;
        public bool IsUpVote { get; private set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }

        private CommentVote(Guid commentId, string fanId, bool isUpVote)
        {
            CommentId = commentId;
            FanId = fanId;
            IsUpVote = isUpVote;

            AddDomainEvent(new CommentVoteAddedDomainEvent(CommentId, FanId, IsUpVote));
        }

        public static Result<CommentVote> Create(Guid commentId, string fanId, bool isUpVote)
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
    }
}
