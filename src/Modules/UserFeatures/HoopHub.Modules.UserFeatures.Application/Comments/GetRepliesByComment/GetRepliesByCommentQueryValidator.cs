using FluentValidation;

namespace HoopHub.Modules.UserFeatures.Application.Comments.GetRepliesByComment
{
    public class GetRepliesByCommentQueryValidator : AbstractValidator<GetRepliesByCommentQuery>
    {
        public GetRepliesByCommentQueryValidator()
        {
            RuleFor(x => x.CommentId).NotEmpty();
        }
    }
}
