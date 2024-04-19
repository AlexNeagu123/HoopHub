using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Domain.Rules
{
    public class CommentIdCannotBeEmpty(Guid commentId) : IBusinessRule
    {
        private readonly Guid _commentId = commentId;

        public bool IsBroken() => _commentId == Guid.Empty;

        public string Message => ValidationErrors.InvalidCommentId;
    }
}
