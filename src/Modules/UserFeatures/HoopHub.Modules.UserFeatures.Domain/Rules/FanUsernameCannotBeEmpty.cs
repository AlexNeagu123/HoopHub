using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Domain.Rules
{
    public class FanUsernameCannotBeEmpty(string fanUsername) : IBusinessRule
    {
        private readonly string _fanUsername = fanUsername;
        public bool IsBroken() => string.IsNullOrWhiteSpace(_fanUsername);
        public string Message => ValidationErrors.InvalidUsername;
    }
}
