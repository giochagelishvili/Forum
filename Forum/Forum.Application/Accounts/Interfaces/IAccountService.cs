using Forum.Application.Accounts.Requests;
using Forum.Domain;

namespace Forum.Application.Accounts.Interfaces
{
    public interface IAccountService
    {
        Task<bool> IsAdminAsync(string username, UserRole role);
        Task SignInAsync(LoginRequestModel model);
        Task RegisterAsync(RegisterRequestModel model);
        Task SignOutAsync();
    }
}
