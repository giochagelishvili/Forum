using FluentValidation;
using Forum.Application.Comments.Requests;
using Forum.Shared.Localizations;

namespace Forum.Shared.Validators.Comments
{
    public class CommentRequestPostModelValidator : AbstractValidator<CommentRequestPostModel>
    {
        public CommentRequestPostModelValidator()
        {
            RuleFor(comment => comment.Body)
                .NotEmpty()
                .WithMessage(ErrorMessages.CommentBodyRequired);
        }
    }
}
