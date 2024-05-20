using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Domain.Rules
{
    public class PlayerRatingShouldBeDecimalBetween1And10(decimal rating) : IBusinessRule
    {
        public bool IsBroken() => rating is < 0 or > 5;

        public string Message => ValidationErrors.InvalidPlayerRating;
    }
}
