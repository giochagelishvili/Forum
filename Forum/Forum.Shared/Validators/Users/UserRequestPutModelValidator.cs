using FluentValidation;
using Forum.Application.Users.Requests.Updates;
using Forum.Shared.Localizations;

namespace Forum.Shared.Validators.Users
{
    public class UserRequestPutModelValidator : AbstractValidator<UserRequestPutModel>
    {
        public UserRequestPutModelValidator()
        {
            RuleFor(model => model.Email)
                .NotEmpty().When(model => model.Email != null)
                .WithMessage(ErrorMessages.EmailRequired)
                .Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")
                .When(model => model.Email != null)
                .WithMessage(ErrorMessages.InvalidEmailFormat);

            RuleFor(model => model.UpdatedUsername)
                .NotEmpty().When(model => model.UpdatedUsername != null)
                .WithMessage(ErrorMessages.UsernameRequired)
                .MaximumLength(50).When(model => model.UpdatedUsername != null)
                .WithMessage(ErrorMessages.UsernameMaxLength);

            RuleFor(model => model.Bio)
                .NotEmpty().When(model => model.Bio != null)
                .WithMessage(ErrorMessages.EmptyBio);
        }
    }
}
