// UserRepo.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewsPaperAuthExample.Entities;

namespace NewsPaperAuthExample.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly RepoContext _context;

        public UserRepo(RepoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task CreateUserAsync(User user)
        {
            user.PasswordHash = HashPassword(user.PasswordHash);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            if (!string.IsNullOrEmpty(user.PasswordHash))
            {
                user.PasswordHash = HashPassword(user.PasswordHash);
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        private string HashPassword(string password)
        {
            return password;
        }
    }
}
