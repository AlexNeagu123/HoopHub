using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Fans;
using HoopHub.Modules.UserFeatures.Domain.Rules;

namespace HoopHub.Modules.UserFeatures.Domain.Reviews
{
    public class PlayerPerformanceReview : AuditableEntity
    {
        public Guid HomeTeamId { get; private set; }
        public Guid VisitorTeamId { get; private set; }
        public string Date { get; private set; }
        public string FanId { get; private set; }
        public Fan Fan { get; private set; } = null!;
        public decimal Rating { get; private set; }
        public Guid PlayerId { get; private set; }
        private PlayerPerformanceReview(Guid homeTeamId, Guid visitorTeamId, string date, string fanId, decimal rating, Guid playerId)
        {
            HomeTeamId = homeTeamId;
            VisitorTeamId = visitorTeamId;
            Date = date;
            FanId = fanId;
            Rating = rating;
            PlayerId = playerId;
        }

        public static Result<PlayerPerformanceReview> Create(Guid homeTeamId, Guid visitorTeamId, string date, string fanId, decimal rating, Guid playerId)
        {
            try
            {
                CheckRule(new BothTeamIdsAreRequired(homeTeamId));
                CheckRule(new BothTeamIdsAreRequired(visitorTeamId));
                CheckRule(new PlayerIdCannotBeEmpty(playerId));
                CheckRule(new FanIdCannotBeEmpty(fanId));
                CheckRule(new DateMustBeValid(date));
                CheckRule(new PlayerRatingShouldBeDecimalBetween1And10(rating));
            }
            catch (BusinessRuleValidationException e)
            {
                return Result<PlayerPerformanceReview>.Failure(e.Details);
            }

            return Result<PlayerPerformanceReview>.Success(new PlayerPerformanceReview(homeTeamId, visitorTeamId, date, fanId, rating, playerId));
        }

        public void Update(decimal rating)
        {
            try
            {
                CheckRule(new PlayerRatingShouldBeDecimalBetween1And10(rating));
                Rating = rating;
            }
            catch (BusinessRuleValidationException) { }
        }
    }
}
