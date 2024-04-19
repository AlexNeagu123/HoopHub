using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Domain.Rules
{
    public class GameRatingShouldBeDecimalBetween0And5(decimal rating) : IBusinessRule
    {
        private readonly decimal _rating = rating;
        public bool IsBroken() => _rating is < 0 or > 5;
        public string Message => ValidationErrors.InvalidGameRating;
    }
}
