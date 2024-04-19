using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Rules;

namespace HoopHub.Modules.UserFeatures.Domain.Threads
{
    public class GameThread : BaseThread
    {
        public Guid HomeTeamId { get; private set; }
        public Guid VisitorTeamId { get; private set; }
        public string Date { get; private set; }

        private GameThread(Guid homeTeamId, Guid visitorTeamId, string date) 
        {
            HomeTeamId = homeTeamId;
            VisitorTeamId = visitorTeamId;
            Date = date;
        }

        public static Result<GameThread> Create(Guid homeTeamId, Guid visitorTeamId, string date)
        {
            try
            {
                CheckRule(new BothTeamIdsAreRequired(homeTeamId));
                CheckRule(new BothTeamIdsAreRequired(visitorTeamId));
                CheckRule(new DateMustBeValid(date));
            }
            catch (BusinessRuleValidationException e)
            {
                return Result<GameThread>.Failure(e.Details);
            }
            return Result<GameThread>.Success(new GameThread(homeTeamId, visitorTeamId, date));
        }
    }
}
