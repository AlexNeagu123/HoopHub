using FluentValidation;
using HoopHub.Modules.UserAccess.Application.Constants;
using HoopHub.Modules.UserAccess.Domain.Registration;
using HoopHub.Modules.UserAccess.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace HoopHub.Modules.UserAccess.Infrastructure.Services.Registration
{
    internal class RegistrationValidator : AbstractValidator<RegistrationModel>
    {
        public RegistrationValidator(UserManager<ApplicationUser> userManager)
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(ErrorMessages.EmailAddressRequired)
                .EmailAddress().WithMessage(ErrorMessages.InvalidEmailAddress);

            RuleFor(x => x.UserName).NotEmpty().WithMessage(ErrorMessages.UserNameRequired);
            RuleFor(x => x.UserName).MaximumLength(Config.UserNameMaxLength).WithMessage(ErrorMessages.UserNameTooLong);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).WithMessage(ErrorMessages.PasswordLengthViolation);
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage(ErrorMessages.PasswordsDoNotMatch);
            RuleFor(x => x.UserName).MustAsync(async (userName, cancellation) =>
            {
                var user = await userManager.FindByNameAsync(userName);
                return user == null;
            }).WithMessage(ErrorMessages.UserAlreadyExists);
            RuleFor(x => x.Email).MustAsync(async (email, cancellation) =>
            {
                var user = await userManager.FindByEmailAsync(email);
                return user == null;
            }).WithMessage(ErrorMessages.EmailAlreadyExists);
        }
    }
}
