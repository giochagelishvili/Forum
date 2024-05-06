using Forum.Application.Users.Interfaces.Repositories;
using Forum.Domain.Users;
using Forum.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ForumContext context) : base(context)
        {
        }

        public async Task<int> GetUserCommentCountAsync(int userId, CancellationToken cancellationToken)
        {
            return await _dbSet
                .Where(user => user.Id == userId)
                .Select(user => user.Comments.Count())
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbSet
                .Include(user => user.Image)
                .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbSet
                .Include(user => user.Image)
                .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
        }

        public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            return await _dbSet
                .Include(user => user.Image)
                .FirstOrDefaultAsync(user => user.UserName == username, cancellationToken);
        }
    }
}