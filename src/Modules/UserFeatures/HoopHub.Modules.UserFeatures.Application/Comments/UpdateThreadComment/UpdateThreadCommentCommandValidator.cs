using FluentValidation;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Comments.UpdateThreadComment
{
    public class UpdateThreadCommentCommandValidator : AbstractValidator<UpdateThreadCommentCommand>
    {
        public UpdateThreadCommentCommandValidator(IThreadCommentRepository threadCommentRepository, string fanId)
        {
            RuleFor(x => x.Content).NotNull().NotEmpty().Length(Config.ContentMinLength, Config.ContentMaxLength).WithMessage(ValidationErrors.InvalidCommentContent);
            RuleFor(x => x.CommentId).NotNull().NotEmpty().WithMessage(ValidationErrors.InvalidCommentId);
            RuleFor(x => x.CommentId).MustAsync(async (commentId, cancellation) =>
            {
                var commentResult = await threadCommentRepository.FindByIdAsync(commentId);
                return commentResult.IsSuccess && commentResult.Value.FanId == fanId;
            }).WithMessage(ValidationErrors.CommentDoNotExist);
        }
    }
}
