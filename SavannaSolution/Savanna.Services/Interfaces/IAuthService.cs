using Savanna.Services.Models;

namespace Savanna.Services.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Attempts to authenticate a user with the provided username and password.
        /// </summary>
        /// <param name="Username"> The username or email of the user attempting to log in. </param>
        /// <param name="Password"> The password provided for authentication.</param>
        /// <returns> An instance of <see cref="AuthData"/> containing the authentication result, including success status, error messages, and user details if authenticated. </returns>
        Task<AuthData> TryToLogin(string Username, string Password);

        /// <summary>
        /// Attempts to register a new user with the provided details.
        /// </summary>
        /// <param name="Username"> The desired username for the new user. </param>
        /// <param name="Email"> The email address for the new user. </param>
        /// <param name="Password"> The password for the new user account. </param>
        /// <param name="SecurityQuestion"> A security question to enhance account recovery. </param>
        /// <param name="AnswerToSecurityQuestion"> The answer to the provided security question. </param>
        /// <returns> An instance of <see cref="RegistrationData"/> containing the registration result, including success status, error messages, and user details if registration is successful. </returns>
        Task<RegistrationData> TryToRegistrer(string Username, string Email, string Password, string SecurityQuestion, string AnswerToSecurityQuestion);

        /// <summary>
        /// Attempts to change a user's password, verifying the username, email, and security question answer.
        /// </summary>
        /// <param name="Username"> The username of the user requesting the password change. </param>
        /// <param name="Email"> The email address associated with the user's account. </param>
        /// <param name="UpdatedPassword"> The new password to set for the account. </param>
        /// <param name="AnswerToSecurityQuestion"> The answer to the user's security question for verification. </param>
        /// <returns> An instance of <see cref="ChangingPasswordData"/> containing the result of the password change, including success status and error messages. </returns>
        Task<ChangingPasswordData> TryToChangePassword(string Username, string Email, string UpdatedPassword, string AnswerToSecurityQuestion);

        /// <summary>
        /// Updates the online status of a specified user.
        /// </summary>
        /// <param name="username"> The username or email of the user. </param>
        /// <param name="isOnline"> A boolean value indicating the user's online status (true for online, false for offline). </param>
        /// <exception cref="Exception"> Thrown when the user cannot be found in the repository. </exception>
        Task SetUserStatus(string username, bool isOnline);

        /// <summary>
        /// Verifies whether a user exists in the database by checking the provided username, email, 
        /// and the hashed answer to their security question.
        /// </summary>
        /// <param name="username"> The username of the user to validate. </param>
        /// <param name="email"> The email address of the user to validate. </param>
        /// <returns>
        /// Returns <c>true</c> if the user exists and the security question answer is correct; 
        /// otherwise, returns <c>false</c>.
        /// </returns>
        bool IsUserExist(string username, string email);
    }
}
