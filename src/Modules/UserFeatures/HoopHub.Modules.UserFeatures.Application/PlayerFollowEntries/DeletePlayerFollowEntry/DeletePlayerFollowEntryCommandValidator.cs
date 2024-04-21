using FluentValidation;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.PlayerFollowEntries.DeletePlayerFollowEntry
{
    public class DeletePlayerFollowEntryCommandValidator : AbstractValidator<DeletePlayerFollowEntryCommand>
    {
        public DeletePlayerFollowEntryCommandValidator(IPlayerFollowEntryRepository playerFollowEntryRepository, string fanId)
        {
            RuleFor(x => x.PlayerId).NotNull().NotEmpty().WithMessage(ValidationErrors.InvalidPlayerId);
            RuleFor(x => x).MustAsync(async (command, cancellation) =>
            {
                var playerFollowEntryResult = await playerFollowEntryRepository.FindByIdPairIncludingFanAsync(fanId, command.PlayerId);
                return playerFollowEntryResult.IsSuccess;
            }).WithMessage(ValidationErrors.PlayerFollowEntryDoNotExist).WithName(ValidationKeys.PlayerFollowEntry);
        }
    }
}
