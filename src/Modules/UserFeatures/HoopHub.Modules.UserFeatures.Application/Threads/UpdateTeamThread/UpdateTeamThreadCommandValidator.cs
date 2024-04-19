using FluentValidation;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Threads.UpdateTeamThread
{
    public class UpdateTeamThreadCommandValidator : AbstractValidator<UpdateTeamThreadCommand>
    {
        public UpdateTeamThreadCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage(ValidationErrors.InvalidThreadId);
            RuleFor(x => x.Title).NotEmpty().Length(Config.TitleMinLength, Config.TitleMaxLength).WithMessage(ValidationErrors.InvalidTitle);
            RuleFor(x => x.Content).NotEmpty().Length(Config.ContentMinLength, Config.ContentMaxLength).WithMessage(ValidationErrors.InvalidThreadContent);
        }
    }
}
