using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Domain.Rules
{
    public class RecipientIdCannotBeEmpty(string recipientId) : IBusinessRule
    {
        private readonly string _recipientId = recipientId;
        public bool IsBroken() => string.IsNullOrEmpty(_recipientId);
        public string Message => ValidationErrors.InvalidRecipientId;

    }
}
