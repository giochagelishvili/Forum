using FluentValidation;
using Forum.Application.Accounts.Requests;
using Forum.Shared.Localizations;

namespace Forum.Web.Infrastructure.Validators.Accounts
{
    public class RegisterRequestModelValidator : AbstractValidator<RegisterRequestModel>
    {
        public RegisterRequestModelValidator()
        {
            RuleFor(model => model.Email)
                .NotEmpty()
                .WithMessage(ErrorMessages.EmailRequired)
                .Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")
                .WithMessage(ErrorMessages.InvalidEmailFormat);

            RuleFor(model => model.Username)
                .NotEmpty()
                .WithMessage(ErrorMessages.UsernameRequired)
                .MaximumLength(50)
                .WithMessage(ErrorMessages.UsernameMaxLength);

            RuleFor(model => model.Password)
                .NotEmpty()
                .WithMessage(ErrorMessages.PasswordRequired)
                .MinimumLength(6)
                .WithMessage(ErrorMessages.PasswordMinLength)
                .MaximumLength(30)
                .WithMessage(ErrorMessages.PasswordMaxLength)
                .Matches(@"^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d\s]).{6,}$")
                .WithMessage(ErrorMessages.InvalidPasswordFormat);

            RuleFor(model => model.ConfirmPassword)
                .Equal(model => model.Password)
                .WithMessage(ErrorMessages.PasswordsDoNotMatch);
        }
    }
}
