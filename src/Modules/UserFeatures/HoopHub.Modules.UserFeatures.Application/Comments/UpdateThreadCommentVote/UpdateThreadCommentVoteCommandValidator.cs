using FluentValidation;
using HoopHub.Modules.UserFeatures.Application.Persistence;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.Comments.UpdateThreadCommentVote
{
    public class UpdateThreadCommentVoteCommandValidator : AbstractValidator<UpdateThreadCommentVoteCommand>
    {
        public UpdateThreadCommentVoteCommandValidator(IThreadCommentVoteRepository threadCommentVoteRepository,  string fanId)
        {
            RuleFor(x => x.CommentId).NotNull().NotEmpty().WithMessage(ValidationErrors.InvalidCommentId);
            RuleFor(x => x.CommentId).MustAsync(async (commentId, cancellation) =>
            {
                var commentVoteResult = await threadCommentVoteRepository.FindByIdAsyncIncludingAll(commentId, fanId);
                return commentVoteResult.IsSuccess;
            }).WithMessage(ValidationErrors.CommentVoteDoNotExist);
        }
    }
}
