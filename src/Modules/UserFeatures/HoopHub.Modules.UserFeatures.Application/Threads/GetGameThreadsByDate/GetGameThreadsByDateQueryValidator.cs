using FluentValidation;
using HoopHub.Modules.UserFeatures.Domain.Constants;
using HoopHub.Modules.UserFeatures.Domain.Rules;

namespace HoopHub.Modules.UserFeatures.Application.Threads.GetGameThreadsByDate
{
    public class GetGameThreadsByDateQueryValidator : AbstractValidator<GetGameThreadsByDateQuery>
    {
        public GetGameThreadsByDateQueryValidator()
        {
            RuleFor(x => x.Date).Must(DateMustBeValid.BeAValidDate).WithMessage(ValidationErrors.InvalidDate);
        }
    }
}
