using FluentValidation;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.TeamFollowEntries.CreateTeamFollowEntry
{
    public class CreateTeamFollowEntryCommandValidator : AbstractValidator<CreateTeamFollowEntryCommand>
    {
        public CreateTeamFollowEntryCommandValidator(ITeamFollowEntryRepository teamFollowEntryRepository, string fanId)
        {
            RuleFor(x => x.TeamId).NotEmpty().WithMessage(ValidationErrors.InvalidTeamId);
            RuleFor(x => x).MustAsync(async (command, cancellation) =>
            {
                var teamFollowEntryResult = await teamFollowEntryRepository.FindByIdPairIncludingFanAsync(fanId, command.TeamId);
                return !teamFollowEntryResult.IsSuccess;
            }).WithMessage(ValidationErrors.TeamFollowEntryAlreadyExists).WithName(ValidationKeys.TeamFollowEntry);
        }
    }
}
