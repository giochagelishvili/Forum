using Forum.Application.Users.Responses;

namespace Forum.Application.Users.Interfaces.Services
{
    public interface IAdminUserService
    {
        Task<List<UserResponseAdminModel>> GetAllUsersAsync(int callerAdminId, CancellationToken cancellationToken);
        Task<List<UserResponseAdminModel>> GetBannedUsersAsync(CancellationToken cancellationToken);
        Task<UserResponseAdminModel> GetUserAsync(int userId, CancellationToken cancellationToken);
        Task<UserResponseAdminModel> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
        Task BanUserAsync(string userId);
        Task UnbanUserAsync(string userId);
        Task<bool> ExistsAsync(string userId);
    }
}
