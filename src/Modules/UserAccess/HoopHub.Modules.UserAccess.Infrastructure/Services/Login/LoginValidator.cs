using FluentValidation;
using HoopHub.Modules.UserAccess.Application.Constants;
using HoopHub.Modules.UserAccess.Domain.Login;
using HoopHub.Modules.UserAccess.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace HoopHub.Modules.UserAccess.Infrastructure.Services.Login
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator(UserManager<ApplicationUser> userManager) 
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage(ErrorMessages.UserNameRequired);
            RuleFor(x => x.Password).NotEmpty().WithMessage(ErrorMessages.PasswordRequired);
            RuleFor(x => x).MustAsync(async (model, cancellation) =>
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                if (user == null)
                    return false;
                var userWithPasswordExists = await userManager.CheckPasswordAsync(user, model.Password);
                return userWithPasswordExists;
            }).WithMessage(ErrorMessages.InvalidCredentials).WithName(ValidationKeys.Credentials);
        }
    }
}
