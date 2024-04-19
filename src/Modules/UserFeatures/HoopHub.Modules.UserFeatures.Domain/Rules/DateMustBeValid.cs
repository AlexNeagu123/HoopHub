using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Domain.Rules
{
    public class DateMustBeValid(string date) : IBusinessRule
    {
        private readonly string _date = date;

        public bool IsBroken()
        {
            return string.IsNullOrEmpty(_date) || DateTime.TryParseExact(_date, Config.DateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out _);
        }

        public string Message => ValidationErrors.InvalidDate;
    }
}
