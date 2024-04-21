using FluentValidation;
using HoopHub.Modules.UserFeatures.Application.Constants;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Comments.DeleteThreadComment
{
    public class DeleteThreadCommentCommandValidator : AbstractValidator<DeleteThreadCommentCommand>
    {
        public DeleteThreadCommentCommandValidator(IThreadCommentRepository threadCommentRepository, string fanId)
        {
            RuleFor(x => x.CommentId).NotEmpty().NotNull().WithMessage(ValidationErrors.InvalidCommentId);
            RuleFor(x => x.CommentId).MustAsync(async (commentId, cancellation) =>
            {
                var commentResult = await threadCommentRepository.FindByIdAsync(commentId);
                return commentResult.IsSuccess && commentResult.Value.FanId == fanId;
            }).WithMessage(ValidationErrors.CommentDoNotExist).WithName(ValidationKeys.ThreadComment);
        }
    }
}
