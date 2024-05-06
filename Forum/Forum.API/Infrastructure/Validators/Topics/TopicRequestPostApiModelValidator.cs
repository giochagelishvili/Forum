using FluentValidation;
using Forum.API.Infrastructure.Models.Topics;
using Forum.Shared.Localizations;

namespace Forum.API.Infrastructure.Validators.Topics
{
    public class TopicRequestPostApiModelValidator : AbstractValidator<TopicRequestPostApiModel>
    {
        public TopicRequestPostApiModelValidator()
        {
            RuleFor(model => model.Title)
                .NotEmpty()
                .WithMessage(ErrorMessages.TitleRequired)
                .MaximumLength(50)
                .WithMessage(ErrorMessages.TitleMaxLength);

            RuleFor(model => model.Description)
                .NotEmpty()
                .WithMessage(ErrorMessages.DescriptionRequired);
        }
    }
}
