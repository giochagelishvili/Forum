using Forum.Application.Comments.Interfaces;
using Forum.Application.Comments.Requests;
using Forum.Application.Exceptions;
using Forum.Application.Topics.Interfaces.Services;
using Forum.Domain.Comments;
using Mapster;

namespace Forum.Application.Comments
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ITopicService _topicService;

        public CommentService(ICommentRepository commentRepository, ITopicService topicService)
        {
            _commentRepository = commentRepository;
            _topicService = topicService;
        }

        public async Task CreateAsync(CommentRequestPostModel comment, CancellationToken cancellationToken)
        {
            if (!await _topicService.ExistsAsync(comment.TopicId, cancellationToken))
                throw new TopicNotFoundException();

            if (!await _topicService.IsActiveAsync(comment.TopicId, cancellationToken))
                throw new InactiveTopicException();

            var entity = comment.Adapt<Comment>();

            await _commentRepository.CreateAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(int commentId, int authorId, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetAsync(commentId, cancellationToken);

            if (comment == null || comment.AuthorId != authorId)
                throw new CommentNotFoundException();

            await _commentRepository.DeleteAsync(commentId, cancellationToken);
        }
    }
}
