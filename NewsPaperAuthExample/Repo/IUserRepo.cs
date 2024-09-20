
using NewsPaperAuthExample.Entities;

namespace NewsPaperAuthExample.Repo
{
    public interface IUserRepo
    {
        /// <summary>
        /// Get all the users from the DB
        /// </summary>
        /// <returns>A list of all users</returns>
        Task<IEnumerable<User>> GetAllUsersAsync();

        /// <summary>
        /// Gets an user by it's id
        /// </summary>
        /// <param name="id">the id of the user being fetched</param>
        /// <returns>returns the fetched user</returns>
        Task<User> GetUserByIdAsync(int id);

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="user">The parameters of the user being created</param>
        /// <returns></returns>
        Task CreateUserAsync(User user, string roleName);

        /// <summary>
        /// Updates the user
        /// </summary>
        /// <param name="user">The user being updated</param>
        /// <returns>The updated user</returns>
        Task UpdateUserAsync(User user);

        Task<List<string>> GetRolesAsync(User user);

        Task<User> GetUserByEmailAsync(string email);
        Task<bool> ValidateUserCredentialsAsync(string email, string password);





    }
}
