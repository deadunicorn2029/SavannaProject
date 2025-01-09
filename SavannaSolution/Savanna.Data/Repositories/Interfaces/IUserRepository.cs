using Savanna.Data.Data;

namespace Savanna.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a user from the database by their unique identifier.
        /// </summary>
        /// <param name="id"> The unique identifier (UserId) of the user to retrieve. </param>
        /// <returns> A <see cref="Task{TResult}"/> representing the asynchronous operation, with a result of <see cref="User"/> if found, or null if no user matches the provided ID.</returns>
        Task<User> GetUserByIdAsync(int id);

        Task<User> GetUserByUsernameOrEmail(string username);

        Task<User> GetUserByUsernameAndEmail(string username, string email);

        void Update(User user);

        void Save(User user);

        /// <summary>
        /// Creates and saves a new user in the database with the specified details.
        /// </summary>
        /// <param name="username"> The username of the user to register. </param>
        /// <param name="email"> The email address of the user to register. </param>
        /// <param name="hashedPassword"> The hashed password of the user. </param>
        /// <param name="securityQuestion"> The security question chosen by the user. </param>
        /// <param name="hashedAnswer"> The hashed answer to the user's security question. </param>
        void Insert(User user);
    }
}
