using Forum.Application.Users.Interfaces.Services;
using Forum.Application.Users.Responses;

namespace Forum.Workers.Bans
{
    public class BanService
    {
        private readonly IAdminUserService _adminUserService;

        public BanService(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        public async Task<List<UserResponseAdminModel>> GetBannedUsersAsync(CancellationToken cancellationToken)
        {
            return await _adminUserService.GetBannedUsersAsync(cancellationToken);
        }

        public async Task UnbanUser(string id)
        {
            await _adminUserService.UnbanUserAsync(id);
        }
    }
}
