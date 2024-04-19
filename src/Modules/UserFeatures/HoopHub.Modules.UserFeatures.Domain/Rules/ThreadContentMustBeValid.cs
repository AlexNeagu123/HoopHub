using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Domain.Rules
{
    public class ThreadContentMustBeValid(string content) : IBusinessRule
    {
        private readonly string _content = content;
        public bool IsBroken() => string.IsNullOrWhiteSpace(_content) || _content.Length < Config.ContentMinLength || _content.Length > Config.ContentMaxLength;
        public string Message => ValidationErrors.InvalidThreadContent;
    }
}
