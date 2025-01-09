using Savanna.Services.Models;
using Savanna.Services.Interfaces;
using Savanna.Services.Helpers;
using Savanna.Data;
using Savanna.Data.Data;
using System.Text.RegularExpressions;
using System.Linq;
using Savanna.Data.Repositories.Implementations;
using Savanna.Data.Repositories.Interfaces;

namespace Savanna.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region IsUserExist
        public bool IsUserExist(string username, string email)
        {
            var result = false;

            try
            {
                var userExistInDB = _userRepository.GetUserByUsernameAndEmail(username, email);

                if (userExistInDB == null)
                {
                    return result;
                }

                return true;
            }
            catch (Exception ex)
            {
               return false;
            }
        }
        #endregion IsUserExist

        #region TryToLogin
        public async Task<AuthData> TryToLogin(string Username, string Password)
        {
            var result = new AuthData();

            try
            {
                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                {
                    result.ErrorMessage = ServicesTextConstants.UsernamePasswordEmpty;

                    result.IsAuthentificated = false;
                    return result;
                }

                var userExistInDB = await _userRepository.GetUserByUsernameOrEmail(Username);

                if (userExistInDB == null)
                {
                    result.ErrorMessage = ServicesTextConstants.UserNotFound;

                    result.IsAuthentificated = false;
                    return result;
                }

                if (!Hasher.Verify(Password, userExistInDB.Password))
                {
                    result.ErrorMessage = ServicesTextConstants.IncorrectPassword;

                    result.IsAuthentificated = false;
                    return result;
                }

                result.IsAuthentificated = true;
                result.UserName = userExistInDB.UserName;
                result.Email = userExistInDB.Email;

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage += ex.ToString();
                result.IsAuthentificated = false;
                return result;
            }
        }
        #endregion TryToLogin

        #region SetUserStatus
        public async Task SetUserStatus(string username, bool isOnline)
        {
            var user = await _userRepository.GetUserByUsernameOrEmail(username);
            
            if(user == null)
            {
                throw new Exception(ServicesTextConstants.UserNotFound);
            }

            user.IsOnline = isOnline;
            _userRepository.Update(user);
            _userRepository.Save(user);
        }
        #endregion SetUserStatus

        #region TryToRegaster
        public async Task<RegistrationData> TryToRegistrer(string Username, string Email, string Password, string SecurityQuestion, string AnswerToSecurityQuestion)
        {
            var result = new RegistrationData();

            try
            {
                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                {
                    result.ErrorMessage = ServicesTextConstants.UsernamePasswordEmpty;

                    result.IsRegistred = false;
                    return result;
                }

                if (string.IsNullOrEmpty(SecurityQuestion))
                {
                    result.ErrorMessage = ServicesTextConstants.EmptySecurityQuestion;

                    result.IsRegistred = false;
                    return result;
                }

                if (string.IsNullOrEmpty(AnswerToSecurityQuestion))
                {
                    result.ErrorMessage = ServicesTextConstants.EmptyAnserToSecurityQuestion;

                    result.IsRegistred = false;
                    return result;
                }

                var userExistInDB = await _userRepository.GetUserByUsernameAndEmail(Username, Email);

                if (userExistInDB != null && userExistInDB.UserName.Equals(Username))
                {
                    result.ErrorMessage = ServicesTextConstants.UsernameTaken;

                    result.IsRegistred = false;
                    return result;
                }

                if (userExistInDB != null && userExistInDB.Email.Equals(Email))
                {
                    result.ErrorMessage = ServicesTextConstants.EmailTaken;

                    result.IsRegistred = false;
                    return result;
                }

                result.IsRegistred = true;
                result.UserName = Username;
                result.Email = Email;
                result.SecurityQuestion = SecurityQuestion;

                var user = new User
                {
                    UserName = Username,
                    Email = Email,
                    Password = Hasher.Hash(Password),
                    SecurityQuestion = SecurityQuestion,
                    AnswerToSecurityQuestion = Hasher.Hash(AnswerToSecurityQuestion)
                };
                
                _userRepository.Insert(user);

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage += ex.ToString();
                result.IsRegistred = false;
                return result;
            }
        }

        #endregion TryToRegaster

        #region TryToChangePassword
        public async Task<ChangingPasswordData> TryToChangePassword(string Username, string Email, string UpdatedPassword, string AnswerToSecurityQuestion)
        {
            var result = new ChangingPasswordData();

            try
            {
                if (string.IsNullOrEmpty(Username))
                {
                    result.ErrorMessage = ServicesTextConstants.UsernameEmpty;

                    result.IsPasswordChanged = false;
                    return result;
                }

                if (string.IsNullOrEmpty(Email))
                {
                    result.ErrorMessage = ServicesTextConstants.EmailEmpty;

                    result.IsPasswordChanged = false;
                    return result;
                }

                if (string.IsNullOrEmpty(AnswerToSecurityQuestion))
                {
                    result.ErrorMessage = ServicesTextConstants.EmptyAnserToSecurityQuestion;

                    result.IsPasswordChanged = false;
                    return result;
                }

                if (string.IsNullOrEmpty(UpdatedPassword))
                {
                    result.ErrorMessage = ServicesTextConstants.PasswordEmpty;

                    result.IsPasswordChanged = false;
                    return result;
                }

                var regex = ServicesTextConstants.Regex;

                Match match = Regex.Match(UpdatedPassword, regex, RegexOptions.IgnoreCase);

                if (UpdatedPassword.Trim().Length < 8 || !match.Success)
                {
                    result.ErrorMessage = ServicesTextConstants.PasswordIncorrectCriteria;

                    result.IsPasswordChanged = false;
                    return result;
                }

                var userExistInDB = await _userRepository.GetUserByUsernameAndEmail(Username, Email);

                if (!Hasher.Verify(AnswerToSecurityQuestion, userExistInDB.AnswerToSecurityQuestion))
                {
                    result.ErrorMessage = ServicesTextConstants.AnswerIncorrect;

                    result.IsPasswordChanged = false;
                    return result;
                }

                if (userExistInDB == null)
                {
                    result.ErrorMessage = ServicesTextConstants.UserNotFound;

                    result.IsPasswordChanged = false;
                    return result;
                }

                UpdatedPassword = Hasher.Hash(UpdatedPassword);
                userExistInDB.Password = UpdatedPassword;
                _userRepository.Update(userExistInDB);
                _userRepository.Save(userExistInDB);
                result.IsPasswordChanged = true;

                return result;
            }
            catch (Exception ex)
            {
                result.ErrorMessage += ex.ToString();
                result.IsPasswordChanged = false;
                return result;
            }
        }
        #endregion TryToChangePassword
    }
}
