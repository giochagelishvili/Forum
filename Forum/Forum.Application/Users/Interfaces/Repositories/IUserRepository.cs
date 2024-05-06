using Forum.Domain.Users;

namespace Forum.Application.Users.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<int> GetUserCommentCountAsync(int userId, CancellationToken cancellationToken);
        Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
