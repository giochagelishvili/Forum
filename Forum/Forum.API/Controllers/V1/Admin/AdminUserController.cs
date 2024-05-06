using Forum.Application.Users.Interfaces.Services;
using Forum.Application.Users.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.API.Controllers.V1.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class AdminUserController : CustomControllerBase
    {
        private readonly IAdminUserService _adminUserService;

        public AdminUserController(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        [HttpGet]
        public async Task<List<UserResponseAdminModel>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            var callerAdminId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            return await _adminUserService.GetAllUsersAsync(callerAdminId, cancellationToken);
        }

        [HttpGet("{userId}")]
        public async Task<UserResponseAdminModel> GetUserAsync(int userId, CancellationToken cancellationToken)
        {
            return await _adminUserService.GetUserAsync(userId, cancellationToken);
        }

        [HttpGet("email")]
        public async Task<UserResponseAdminModel> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _adminUserService.GetUserByEmailAsync(email, cancellationToken);
        }

        [HttpPost("ban")]
        public async Task BanUserAsync(string userId)
        {
            await _adminUserService.BanUserAsync(userId);
        }

        [HttpPost("unban")]
        public async Task UnbanUserAsync(string userId)
        {
            await _adminUserService.UnbanUserAsync(userId);
        }
    }
}
