using FluentValidation;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Threads.DeleteTeamThread
{
    public class DeleteTeamThreadCommandValidator : AbstractValidator<DeleteTeamThreadCommand>
    {
        public DeleteTeamThreadCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(ValidationErrors.InvalidThreadId);
        }
    }
}
