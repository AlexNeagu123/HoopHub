using FluentValidation;
using HoopHub.Modules.UserAccess.Domain.Registration;

namespace HoopHub.Modules.UserAccess.Infrastructure.Services.Registration
{
    internal class RegistrationValidator : AbstractValidator<RegistrationModel>
    {
        public RegistrationValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email address should not be null");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).WithMessage("Password should have at lease 8 characters");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords don't match");
        }
    }
}
