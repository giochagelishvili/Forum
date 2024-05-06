using Forum.Application.Users.Interfaces.Repositories;
using Forum.Domain.Users;
using Forum.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Forum.Infrastructure.Users.Admin
{
    public class AdminUserRepository : BaseRepository<User>, IAdminUserRepository
    {
        public AdminUserRepository(ForumContext context) : base(context)
        {
        }

        public async Task<List<User>> GetAllUsersAsync(int callerAdminId, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(user => user.Id != callerAdminId)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<User>> GetBannedUsersAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking()
                .Where(user => user.IsBanned && user.BannedUntil <= DateTime.UtcNow)
                .ToListAsync(cancellationToken);
        }

        public async Task<User?> GetUserAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbSet.Include(user => user.Image)
                .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbSet.Include(user => user.Image)
                .FirstOrDefaultAsync(user => user.Email == email, cancellationToken);
        }
    }
}
