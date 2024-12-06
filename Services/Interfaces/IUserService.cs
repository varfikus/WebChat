using WebChat.Models;

namespace WebChat.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserAsync(string token);
        Task<User> GetUserAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<User> AuthenticateAsync(string email, string password);
        Task<User> AuthenticateUserAsync(string email, string password);
    }
}
