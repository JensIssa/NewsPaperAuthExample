using NewsPaperAuthExample.Entities;

namespace NewsPaperAuthExample.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task CreateUserAsync(User user, string roleName);
        Task UpdateUserAsync(User user);

        Task<List<string>> GetRolesAsync(User user);

        Task<User> GetUserByEmailAsync(string email);
        Task<bool> ValidateUserCredentialsAsync(string email, string password);
    }
}
