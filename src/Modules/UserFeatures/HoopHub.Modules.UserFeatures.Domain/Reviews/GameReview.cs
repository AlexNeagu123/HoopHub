using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Fans;
using HoopHub.Modules.UserFeatures.Domain.Rules;

namespace HoopHub.Modules.UserFeatures.Domain.Reviews
{
    public class GameReview : AuditableEntity
    {
        public Guid HomeTeamId { get; private set; }
        public Guid VisitorTeamId { get; private set; }
        public string Date { get; private set; }
        public string FanId { get; private set; }
        public Fan Fan { get; private set; } = null!;
        public decimal Rating { get; private set; }

        private GameReview(Guid homeTeamId, Guid visitorTeamId, string date, string fanId, decimal rating)
        {
            HomeTeamId = homeTeamId;
            VisitorTeamId = visitorTeamId;
            Date = date;
            FanId = fanId;
            Rating = rating;
        }

        public static Result<GameReview> Create(Guid homeTeamId, Guid visitorTeamId, string date, string fanId,
            decimal rating)
        {
            try
            {
                CheckRule(new BothTeamIdsAreRequired(homeTeamId));
                CheckRule(new BothTeamIdsAreRequired(visitorTeamId));
                CheckRule(new FanIdCannotBeEmpty(fanId));
                CheckRule(new DateMustBeValid(date));
                CheckRule(new GameRatingShouldBeDecimalBetween0And5(rating));
            }
            catch (BusinessRuleValidationException e)
            {
                return Result<GameReview>.Failure(e.Details);
            }
            return Result<GameReview>.Success(new GameReview(homeTeamId, visitorTeamId, date, fanId, rating));
        }
    }
}
