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

        public async Task CreateUserAsync(User user)
        {
            await _userRepo.CreateUserAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepo.UpdateUserAsync(user);
        }
    }
}
