using FluentValidation;
using HoopHub.Modules.UserAccess.Domain.Registration;
using HoopHub.Modules.UserAccess.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace HoopHub.Modules.UserAccess.Infrastructure.Services.Registration
{
    internal class RegistrationValidator : AbstractValidator<RegistrationModel>
    {
        public RegistrationValidator(UserManager<ApplicationUser> userManager)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Please enter a valid email address");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username can't be empty");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).WithMessage("Password should have at lease 8 characters");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords don't match");
            RuleFor(x => x.UserName).MustAsync(async (userName, cancellation) =>
            {
                var user = await userManager.FindByNameAsync(userName);
                return user == null;
            }).WithMessage("Username is already taken");
            RuleFor(x => x.Email).MustAsync(async (email, cancellation) =>
            {
                var user = await userManager.FindByEmailAsync(email);
                return user == null;
            }).WithMessage("Email is already taken");
        }
    }
}
