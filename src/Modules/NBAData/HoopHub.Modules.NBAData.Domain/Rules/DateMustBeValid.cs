using System.Globalization;
using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.NBAData.Domain.Constants;

namespace HoopHub.Modules.NBAData.Domain.Rules
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
            return DateTime.TryParseExact(
                date,
                Config.DateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out _);
        }

        public string Message => ValidationErrors.InvalidDate;
    }
}