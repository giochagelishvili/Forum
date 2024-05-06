using Forum.Domain.Comments;

namespace Forum.Application.Comments.Interfaces
{
    public interface ICommentRepository
    {
        Task<int> GetUserCommentCountAsync(int userId, CancellationToken cancellationToken);
        Task<Comment?> GetAsync(int id, CancellationToken cancellationToken);
        Task CreateAsync(Comment comment, CancellationToken cancellationToken);
        Task DeleteAsync(int commentId, CancellationToken cancellationToken);
    }
}
