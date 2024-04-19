using FluentValidation;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Comments.CreateThreadComment
{
    public class CreateThreadCommentCommandValidator : AbstractValidator<CreateThreadCommentCommand>
    {
        public CreateThreadCommentCommandValidator()
        {
            RuleFor(x => x.Content).NotEmpty().Length(Config.ContentMinLength, Config.ContentMaxLength).WithMessage(ValidationErrors.InvalidCommentContent);
            RuleFor(x => x.TeamThreadId).Must((command, teamThreadId) => (teamThreadId == null) ^ (command.GameThreadId == null)).WithMessage(ValidationErrors.ShouldBeExactlyOneThreadNonNull);
        }
    }
}
