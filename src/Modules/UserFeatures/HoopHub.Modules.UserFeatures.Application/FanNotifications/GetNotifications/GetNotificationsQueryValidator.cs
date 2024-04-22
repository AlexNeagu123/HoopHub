using FluentValidation;
using HoopHub.Modules.UserFeatures.Domain.Constants;

namespace HoopHub.Modules.UserFeatures.Application.FanNotifications.GetNotifications
{
    public class GetNotificationsQueryValidator : AbstractValidator<GetNotificationsQuery>
    {
        public GetNotificationsQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0).WithMessage(ValidationErrors.InvalidPage);
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage(ValidationErrors.InvalidPageSize);
        }
    }
}
