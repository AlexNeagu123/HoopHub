using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Rules;

namespace HoopHub.Modules.UserFeatures.Domain.Threads
{
    public class GameThread : BaseThread
    {
        public int HomeTeamApiId { get; private set; }
        public int VisitorTeamApiId { get; private set; }
        public string Date { get; private set; }

        private GameThread(int homeTeamApiId, int visitorTeamApiId, string date) 
        {
            HomeTeamApiId = homeTeamApiId;
            VisitorTeamApiId = visitorTeamApiId;
            Date = date;
        }

        public static Result<GameThread> Create(int homeTeamApiId, int visitorTeamApiId, string date)
        {
            try
            {
                CheckRule(new DateMustBeValid(date));
            }
            catch (BusinessRuleValidationException e)
            {
                return Result<GameThread>.Failure(e.Details);
            }
            return Result<GameThread>.Success(new GameThread(homeTeamApiId, visitorTeamApiId, date));
        }
    }
}
