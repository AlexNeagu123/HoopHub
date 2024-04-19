using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Fans;
using HoopHub.Modules.UserFeatures.Domain.Rules;

namespace HoopHub.Modules.UserFeatures.Domain.Follows
{
    public class PlayerFollowEntry : AuditableEntity
    {
        public string FanId { get; private set; }
        public Fan Fan { get; private set; } = null!;
        public Guid PlayerId { get; private set; }

        private PlayerFollowEntry(string fanId, Guid playerId)
        {
            FanId = fanId;
            PlayerId = playerId;
        }

        public static Result<PlayerFollowEntry> Create(string fanId, Guid playerId)
        {
            try
            {
                CheckRule(new FanIdCannotBeEmpty(fanId));
                CheckRule(new PlayerIdCannotBeEmpty(playerId));
            }
            catch (BusinessRuleValidationException e)
            {
                return Result<PlayerFollowEntry>.Failure(e.Details);
            }
            return Result<PlayerFollowEntry>.Success(new PlayerFollowEntry(fanId, playerId));
        }
    }
}
