using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Domain.Rules
{
    public class TitleMustBeValid(string title) : IBusinessRule
    {
        private readonly string _title = title;
        public bool IsBroken() => string.IsNullOrWhiteSpace(_title) || _title.Length < 5 || title.Length > 50;
        public string Message => ValidationErrors.InvalidThreadTitle;
    }
}
