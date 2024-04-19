using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Domain.Rules
{
    public class ParentCommentIdCannotBeEmpty(Guid parentCommentId) : IBusinessRule
    {
        private readonly Guid _parentCommentId = parentCommentId;
        public bool IsBroken() => _parentCommentId == Guid.Empty;
        public string Message => ValidationErrors.InvalidParentCommentId;
    }
}
