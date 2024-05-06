using FluentValidation;
using Forum.Application.Users.Requests.Updates;
using Forum.Shared.Localizations;

namespace Forum.Shared.Validators.Users
{
    public class PasswordRequestPutModelValidator : AbstractValidator<PasswordRequestPutModel>
    {
        public PasswordRequestPutModelValidator()
        {
            RuleFor(model => model)
                .Must(model =>
                    !string.IsNullOrEmpty(model.CurrentPassword) &&
                    !string.IsNullOrEmpty(model.NewPassword) &&
                    !string.IsNullOrEmpty(model.ConfirmPassword))
                .WithMessage(ErrorMessages.AllPasswordsRequired);

            RuleFor(model => model.NewPassword)
                .MaximumLength(30)
                .WithMessage(ErrorMessages.PasswordMaxLength)
                .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{6,}$")
                .WithMessage(ErrorMessages.InvalidPasswordFormat);

            RuleFor(model => model.ConfirmPassword)
                .Equal(model => model.NewPassword)
                .WithMessage(ErrorMessages.PasswordsDoNotMatch);
        }
    }
}
