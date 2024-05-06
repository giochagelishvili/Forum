using FluentValidation;
using Forum.API.Infrastructure.Models.Comments;
using Forum.Shared.Localizations;

namespace Forum.API.Infrastructure.Validators.Comments
{
    public class CommentRequestPostApiModelValidator : AbstractValidator<CommentRequestPostApiModel>
    {
        public CommentRequestPostApiModelValidator()
        {
            RuleFor(comment => comment.TopicId)
                .NotEmpty()
                .WithMessage(ErrorMessages.TopicIdRequired);

            RuleFor(comment => comment.Body)
                .NotEmpty()
                .WithMessage(ErrorMessages.CommentBodyRequired);
        }
    }
}
