using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Domain.Rules
{
    public class PlayerIdCannotBeEmpty(Guid playerId) : IBusinessRule
    {
        private readonly Guid _playerId = playerId;
        public bool IsBroken() => _playerId == Guid.Empty;
        public string Message => ValidationErrors.InvalidPlayerId;
    }
}
