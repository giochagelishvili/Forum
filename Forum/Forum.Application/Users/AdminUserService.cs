using Forum.Application.Exceptions;
using Forum.Application.Users.Interfaces.Repositories;
using Forum.Application.Users.Interfaces.Services;
using Forum.Application.Users.Responses;
using Forum.Domain.Users;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Forum.Application.Users
{
    public class AdminUserService : IAdminUserService
    {
        private readonly IConfiguration _config;
        private readonly IAdminUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public AdminUserService(IConfiguration config, IAdminUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _config = config;
        }

        public async Task<List<UserResponseAdminModel>> GetAllUsersAsync(int callerAdminId, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetAllUsersAsync(callerAdminId, cancellationToken);

            return result.Adapt<List<UserResponseAdminModel>>();
        }

        public async Task<List<UserResponseAdminModel>> GetBannedUsersAsync(CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetBannedUsersAsync(cancellationToken);

            return result.Adapt<List<UserResponseAdminModel>>();
        }

        public async Task<UserResponseAdminModel> GetUserAsync(int userId, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUserAsync(userId, cancellationToken);

            if (result == null)
                throw new UserNotFoundException();

            return result.Adapt<UserResponseAdminModel>();
        }

        public async Task<UserResponseAdminModel> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetUserByEmailAsync(email, cancellationToken);

            if (result == null)
                throw new UserNotFoundException();

            return result.Adapt<UserResponseAdminModel>();
        }

        public async Task BanUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new UserNotFoundException();

            var banDuration = _config.GetValue<int>("Constants:BanDuration");

            user.IsBanned = true;
            user.BannedUntil = DateTime.UtcNow.AddSeconds(10);

            await _userManager.UpdateAsync(user);
        }

        public async Task UnbanUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new UserNotFoundException();

            user.IsBanned = false;
            user.BannedUntil = null;

            await _userManager.UpdateAsync(user);
        }

        public async Task<bool> ExistsAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId) != null;
        }
    }
}
