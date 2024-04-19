using HoopHub.BuildingBlocks.Domain;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Domain.Rules
{
    public class BothTeamIdsAreRequired(Guid teamId) : IBusinessRule
    {
        private readonly Guid _teamId = teamId;
        public bool IsBroken() => _teamId == Guid.Empty;
        public string Message => ValidationErrors.BothTeamIdsRequired;
    }
}
