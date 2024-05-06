using Forum.Domain.Users;

namespace Forum.Application.Users.Interfaces.Repositories
{
    public interface IAdminUserRepository
    {
        Task<List<User>> GetAllUsersAsync(int callerAdminId, CancellationToken cancellationToken);
        Task<List<User>> GetBannedUsersAsync(CancellationToken cancellationToken);
        Task<User?> GetUserAsync(int userId, CancellationToken cancellationToken);
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
