using FluentValidation;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using HoopHub.Modules.UserFeatures.Domain.Rules;

namespace HoopHub.Modules.UserFeatures.Application.Threads.CreateGameThread
{
    public class CreateGameThreadCommandValidator : AbstractValidator<CreateGameThreadCommand>
    {
        public CreateGameThreadCommandValidator(IGameThreadRepository gameThreadRepository)
        {
            RuleFor(x => x.HomeTeamId).NotEmpty().WithMessage(ValidationErrors.BothTeamIdsRequired);
            RuleFor(x => x.VisitorTeamId).NotEmpty().WithMessage(ValidationErrors.BothTeamIdsRequired);
            RuleFor(x => x.Date).Must(DateMustBeValid.BeAValidDate).WithMessage(ValidationErrors.InvalidDate);
            RuleFor(x => x).MustAsync(async (command, cancellation) =>
            {
                var gameThreadResult = await gameThreadRepository.FindByTupleIdAsync(command.HomeTeamId, command.VisitorTeamId, command.Date);
                return !gameThreadResult.IsSuccess;
            }).WithMessage(ValidationErrors.GameThreadExists).WithName(ValidationKeys.GameThread);
        }
    }
}
