using FluentValidation;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Threads.DeleteTeamThread
{
    public class DeleteTeamThreadCommandValidator : AbstractValidator<DeleteTeamThreadCommand>
    {
        public DeleteTeamThreadCommandValidator(ITeamThreadRepository teamThreadRepository)
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(ValidationErrors.InvalidThreadId);
            RuleFor(x => x.Id).MustAsync(async (id, cancellation) =>
            {
                var teamThreadResult = await teamThreadRepository.FindByIdAsync(id);
                return teamThreadResult.IsSuccess;
            }).WithMessage(ValidationErrors.ThreadDoNotExist).WithName(ValidationKeys.TeamThread);
        }
    }
}
