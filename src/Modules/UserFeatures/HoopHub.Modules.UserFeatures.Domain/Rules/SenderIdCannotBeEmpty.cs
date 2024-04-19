using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Domain.Rules
{
    public class SenderIdCannotBeEmpty(string senderId) : IBusinessRule
    {
        private readonly string _senderId = senderId;
        public bool IsBroken() => string.IsNullOrEmpty(_senderId);
        public string Message => ValidationErrors.InvalidSenderId;

    }
}
