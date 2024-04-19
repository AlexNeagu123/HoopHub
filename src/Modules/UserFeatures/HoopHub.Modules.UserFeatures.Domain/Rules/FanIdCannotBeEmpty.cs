using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Domain.Rules
{
    public class FanIdCannotBeEmpty(string fanId) : IBusinessRule
    {
        private readonly string _fanId = fanId;
        public bool IsBroken() => string.IsNullOrWhiteSpace(_fanId);
        public string Message => ValidationErrors.InvalidFanId;
    }
}
