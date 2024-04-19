using FluentValidation;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Threads.CreateTeamThread
{
    public class CreateTeamThreadCommandValidator : AbstractValidator<CreateTeamThreadCommand>
    {
        public CreateTeamThreadCommandValidator()
        {
            RuleFor(x => x.TeamId).NotEmpty().WithMessage(ValidationErrors.InvalidTeamId);
            RuleFor(x => x.Title).NotEmpty().Length(Config.TitleMinLength, Config.TitleMaxLength).WithMessage(ValidationErrors.InvalidTitle);
            RuleFor(x => x.Content).NotEmpty().Length(Config.ContentMinLength, Config.ContentMaxLength).WithMessage(ValidationErrors.InvalidThreadContent);
        }
    }
}
