using Forum.API.Controllers.V1.Admin;
using Forum.Application.Users.Interfaces.Services;
using Forum.Application.Users.Requests.Updates;
using Forum.Application.Users.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Forum.API.Controllers.V1
{
    [Authorize]
    [ApiController]
    public class UserController : CustomControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet("email")]
        public async Task<UserResponseModel> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _userService.GetByEmailAsync(email, cancellationToken);
        }

        [HttpGet("username")]
        public async Task<UserResponseModel> GetByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            return await _userService.GetByUsernameAsync(username, cancellationToken);
        }

        [HttpGet("id")]
        public async Task<UserResponseModel> GetByUsernameAsync(int id, CancellationToken cancellationToken)
        {
            return await _userService.GetByIdAsync(id, cancellationToken);
        }

        [HttpPost("update")]
        public async Task UpdateAsync(UserRequestPutModel putModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _userService.UpdateAsync(putModel, userId);
        }

        [HttpPost("changePassword")]
        public async Task ChangePasswordAsync(PasswordRequestPutModel putModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _userService.ChangePasswordAsync(putModel, userId);
        }

        [HttpDelete("deleteGender")]
        public async Task DeleteGenderAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _userService.DeleteGenderAsync(userId);
        }
    }
}
