using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Domain.Rules
{
    public class ThreadIdCannotBeEmpty(Guid threadId) : IBusinessRule
    {
        public bool IsBroken() => threadId == Guid.Empty;
        public string Message => ValidationErrors.InvalidThreadId;
    }
}
