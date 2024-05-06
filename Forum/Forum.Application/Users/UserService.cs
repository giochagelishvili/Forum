using Forum.Application.Exceptions;
using Forum.Application.Users.Interfaces.Repositories;
using Forum.Application.Users.Interfaces.Services;
using Forum.Application.Users.Requests.Updates;
using Forum.Application.Users.Responses;
using Forum.Domain.Users;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Forum.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(IUserRepository userRepository, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<List<string>> GetUserRolesAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var roles = await _userManager.GetRolesAsync(user);

            return roles.Adapt<List<string>>();
        }

        public async Task UpdateAsync(UserRequestPutModel updateModel, string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                throw new UserNotFoundException();

            if (updateModel.Email != null && await _userManager.FindByEmailAsync(updateModel.Email) != null)
                throw new EmailAlreadyExistsException();

            if (updateModel.UpdatedUsername != null && await _userManager.FindByNameAsync(updateModel.UpdatedUsername) != null)
                throw new UsernameAlreadyExistsException();

            if (updateModel.Email != null)
                user.Email = updateModel.Email;

            if (updateModel.UpdatedUsername != null)
                user.UserName = updateModel.UpdatedUsername;

            if (updateModel.Gender != null)
                user.Gender = updateModel.Gender;

            if (updateModel.Bio != null)
                user.Bio = updateModel.Bio;

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
        }

        public async Task DeleteGenderAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                throw new UserNotFoundException();

            user.Gender = null;

            await _userManager.UpdateAsync(user);
        }

        public async Task ChangePasswordAsync(PasswordRequestPutModel passwordModel, string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            await _userManager.ChangePasswordAsync(user, passwordModel.CurrentPassword, passwordModel.NewPassword);
        }

        public async Task<UserResponseModel> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetByIdAsync(id, cancellationToken);

            if (result == null)
                throw new UserNotFoundException();

            return result.Adapt<UserResponseModel>();
        }

        public async Task<UserResponseModel> GetByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetByUsernameAsync(username, cancellationToken);

            if (result == null)
                throw new UserNotFoundException();

            return result.Adapt<UserResponseModel>();
        }

        public async Task<UserResponseModel> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetByEmailAsync(email, cancellationToken);

            if (result == null)
                throw new UserNotFoundException();

            return result.Adapt<UserResponseModel>();
        }

        public async Task<int> GetUserCommentCountAsync(int userId, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserCommentCountAsync(userId, cancellationToken);
        }

        public async Task<bool> ExistsAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId) != null;
        }
    }
}
