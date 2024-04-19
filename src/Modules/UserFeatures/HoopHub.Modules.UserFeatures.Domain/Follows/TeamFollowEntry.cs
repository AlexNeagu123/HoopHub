using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Fans;
using HoopHub.Modules.UserFeatures.Domain.Rules;

namespace HoopHub.Modules.UserFeatures.Domain.Follows
{
    public class TeamFollowEntry : AuditableEntity
    {
        public string FanId { get; private set; }
        public Fan Fan { get; private set; } = null!;
        public Guid TeamId { get; private set; }

        private TeamFollowEntry(string fanId, Guid teamId)
        {
            FanId = fanId;
            TeamId = teamId;
        }

        public static Result<TeamFollowEntry> Create(string fanId, Guid teamId)
        {
            try
            {
                CheckRule(new FanIdCannotBeEmpty(fanId));
                CheckRule(new BothTeamIdsAreRequired(teamId));
            }
            catch (BusinessRuleValidationException e)
            {
                return Result<TeamFollowEntry>.Failure(e.Details);
            }
            return Result<TeamFollowEntry>.Success(new TeamFollowEntry(fanId, teamId));
        }
    }
}
