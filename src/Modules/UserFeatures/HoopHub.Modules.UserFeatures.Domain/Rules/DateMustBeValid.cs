using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using System.Globalization;

namespace HoopHub.Modules.UserFeatures.Domain.Rules
{
    public class DateMustBeValid(string date) : IBusinessRule
    {
        private readonly string _date = date;

        public bool IsBroken()
        {
            return string.IsNullOrEmpty(_date) || !BeAValidDate(_date);
        }

        public static bool BeAValidDate(string date)
        {
            if (DateTime.TryParseExact(
                    date,
                    Config.DateFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var parsedDate))
            {
                return parsedDate.Date <= DateTime.Today;
            }

            return false;
        }

        public string Message => ValidationErrors.InvalidDate;
    }
}
