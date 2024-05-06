using FluentValidation;
using Forum.Application.Topics.Requests;
using Forum.Shared.Localizations;

namespace Forum.Shared.Validators.Topics
{
    public class TopicRequestPostModelValidator : AbstractValidator<TopicRequestPostModel>
    {
        public TopicRequestPostModelValidator()
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
