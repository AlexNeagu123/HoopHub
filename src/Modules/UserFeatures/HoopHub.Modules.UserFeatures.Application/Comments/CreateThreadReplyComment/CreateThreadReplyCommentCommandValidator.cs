using FluentValidation;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Comments.CreateThreadReplyComment
{
    public class CreateThreadReplyCommentCommandValidator : AbstractValidator<CreateThreadReplyCommentCommand>
    {
        public CreateThreadReplyCommentCommandValidator(IThreadCommentRepository threadCommentRepository)
        {
            RuleFor(x => x.Content).NotEmpty().Length(Config.ContentMinLength, Config.ContentMaxLength).WithMessage(ValidationErrors.InvalidCommentContent);
            RuleFor(x => x.ParentCommentId).NotEmpty().NotNull().WithMessage(ValidationErrors.InvalidParentCommentId);
            RuleFor(x => x.TeamThreadId).Must((command, teamThreadId) => (teamThreadId == null) ^ (command.GameThreadId == null)).WithMessage(ValidationErrors.ShouldBeExactlyOneThreadNonNull);
            RuleFor(x => x.ParentCommentId).MustAsync(async (parentId, cancellation) =>
            {
                var parentCommentResult = await threadCommentRepository.FindByIdAsync(parentId);
                return parentCommentResult.IsSuccess;
            }).WithMessage(ValidationErrors.CommentDoNotExist);
        }
    }
}
