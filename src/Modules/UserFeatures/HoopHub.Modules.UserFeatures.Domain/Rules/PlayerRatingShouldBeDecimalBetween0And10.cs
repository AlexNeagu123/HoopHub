using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Domain.Rules
{
    public class PlayerRatingShouldBeDecimalBetween0And10(decimal rating) : IBusinessRule
    {
        public bool IsBroken() => rating is <= 0 or > 10;

        public string Message => ValidationErrors.InvalidPlayerRating;
    }
}
