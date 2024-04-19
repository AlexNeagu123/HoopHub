using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Domain.Rules
{
    public class FanEmailCannotBeEmpty(string fanEmail) : IBusinessRule
    {
        private readonly string _fanEmail = fanEmail;
        public bool IsBroken() => string.IsNullOrWhiteSpace(_fanEmail);
        public string Message => ValidationErrors.InvalidEmail;
    }
}
