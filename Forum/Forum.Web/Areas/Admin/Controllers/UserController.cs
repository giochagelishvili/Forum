using Forum.Application.Users.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IAdminUserService _adminUserService;

        public UserController(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        [HttpGet]
        public async Task<IActionResult> Profile(int userId, CancellationToken cancellationToken)
        {
            var user = await _adminUserService.GetUserAsync(userId, cancellationToken);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> GetByEmail([FromForm] string email, CancellationToken cancellationToken)
        {
            var user = await _adminUserService.GetUserByEmailAsync(email, cancellationToken);

            return View(nameof(Profile), user);
        }

        [HttpGet]
        public async Task<IActionResult> Users(CancellationToken cancellationToken)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var users = await _adminUserService.GetAllUsersAsync(userId, cancellationToken);

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> BanUser(string userId)
        {
            await _adminUserService.BanUserAsync(userId);

            return RedirectToAction(nameof(Users));
        }

        [HttpPost]
        public async Task<IActionResult> UnbanUser(string userId)
        {
            await _adminUserService.UnbanUserAsync(userId);

            return RedirectToAction(nameof(Users));
        }
    }
}
