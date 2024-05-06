using Forum.Application.Users.Requests.Updates;
using Forum.Application.Users.Responses;

namespace Forum.Application.Users.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserResponseModel> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<UserResponseModel> GetByUsernameAsync(string username, CancellationToken cancellationToken);
        Task<UserResponseModel> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<int> GetUserCommentCountAsync(int userId, CancellationToken cancellationToken);
        Task<List<string>> GetUserRolesAsync(string userId);
        Task UpdateAsync(UserRequestPutModel updateModel, string id);
        Task DeleteGenderAsync(string id);
        Task ChangePasswordAsync(PasswordRequestPutModel passwordModel, string id);
        Task<bool> ExistsAsync(string id);
    }
}
