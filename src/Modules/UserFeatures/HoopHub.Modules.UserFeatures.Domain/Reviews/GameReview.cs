using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Fans;
using HoopHub.Modules.UserFeatures.Domain.Reviews.Events;
using HoopHub.Modules.UserFeatures.Domain.Rules;

namespace HoopHub.Modules.UserFeatures.Domain.Reviews
{
    public class GameReview : AuditableEntity
    {
        public int HomeTeamId { get; private set; }
        public int VisitorTeamId { get; private set; }
        public string Date { get; private set; }
        public string FanId { get; private set; }
        public Fan Fan { get; private set; } = null!;
        public decimal Rating { get; private set; }
        public string Content { get; private set; }
        private GameReview(int homeTeamId, int visitorTeamId, string date, string fanId, decimal rating, string content)
        {
            HomeTeamId = homeTeamId;
            VisitorTeamId = visitorTeamId;
            Date = date;
            FanId = fanId;
            Rating = rating;
            Content = content;
        }

        public static Result<GameReview> Create(int homeTeamId, int visitorTeamId, string date, string fanId,
            decimal rating, string content)
        {
            try
            {
                CheckRule(new FanIdCannotBeEmpty(fanId));
                CheckRule(new DateMustBeValid(date));
                CheckRule(new GameRatingShouldBeDecimalBetween1And5(rating));
                CheckRule(new CommentContentMustBeValid(content));
            }
            catch (BusinessRuleValidationException e)
            {
                return Result<GameReview>.Failure(e.Details);
            }
            return Result<GameReview>.Success(new GameReview(homeTeamId, visitorTeamId, date, fanId, rating, content));
        }

        public void MarkAsAdded()
        {
            AddDomainEvent(new GameReviewsCountUpdatedDomainEvent(+1, FanId));
        }

        public void Update(decimal rating, string content)
        {
            try
            {
                CheckRule(new GameRatingShouldBeDecimalBetween1And5(rating));
                CheckRule(new CommentContentMustBeValid(content));
                Content = content;
                Rating = rating;
            }
            catch (BusinessRuleValidationException) { }
        }
    }
}
