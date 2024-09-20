using NewsPaperAuthExample.Entities;
using NewsPaperAuthExample.Repo;

namespace NewsPaperAuthExample.Service
{
    public class UserService: IUserService
    {
        private readonly IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepo.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepo.GetUserByIdAsync(id);
        }

        public async Task CreateUserAsync(User user, string roleName)
        {
            await _userRepo.CreateUserAsync(user, roleName);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepo.UpdateUserAsync(user);
        }

        public async Task<List<string>> GetRolesAsync(User user)
        {
            return await _userRepo.GetRolesAsync(user);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepo.GetUserByEmailAsync(email);
        }

        public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
        {
            return await _userRepo.ValidateUserCredentialsAsync(email, password);
        }
    }
}
