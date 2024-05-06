using Forum.Application.Comments.Requests;

namespace Forum.Application.Comments.Interfaces
{
    public interface ICommentService
    {
        Task CreateAsync(CommentRequestPostModel comment, CancellationToken cancellationToken);
        Task DeleteAsync(int id, int authorId, CancellationToken cancellationToken);
    }
}
