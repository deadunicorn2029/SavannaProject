using Microsoft.AspNetCore.Mvc;
using SavannaWeb.ViewModels;
using Savanna.Data.Data;
using Savanna.Data;
using Savanna.Services.Helpers;
using Savanna.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Savanna.Data.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace SavannaWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ProfileViewModel _profileViewModel;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Displays the Login view for user authentification.
        /// </summary>
        /// <returns> The Login view. </returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Handles the login form submission.
        /// </summary>
        /// <param name="model"> The login view model containing user inout. </param>
        /// <returns> Redirects to the Home page(if valid) </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var registrationData = await _authService.TryToLogin(model.UsernameOrEmail, model.Password);

                if (registrationData.IsAuthentificated)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, registrationData.UserName),
                        new Claim(ClaimTypes.Email, registrationData.Email)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties()
                    {
                        IsPersistent = model.RememberMe
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    _authService.SetUserStatus(registrationData.UserName, true);

                    return RedirectToAction("Index","Game");
                }
                ModelState.AddModelError(string.Empty, "Invalid username/email or password.");
            }
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            var username = User.Identity.Name;
            _authService.SetUserStatus(username, false);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Displays the Register view for new user registration.
        /// </summary>
        /// <returns> Register view. </returns>
        [HttpGet]
        public IActionResult Register()
        {
            var securityQuestions = SecurityQuestions.Questions.Select(q => new SelectListItem
            {
                Text = q,
                Value = q
            }).ToList();
            ViewBag.SecurityQuestions = securityQuestions;
            return View();
        }

        /// <summary>
        /// Handles user registration by validating the input model, delegating user creation to the repository, and signing in the registered user.
        /// </summary>
        /// <param name="model"> The registration details provided by the user. </param>
        /// <returns>
        /// Redirects to the Welcome page on successful registration or re-displays the registration view with errors if validation fails.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var registrationData = await _authService.TryToRegistrer(model.Username, model.Email, model.Password, model.SecurityQuestion, model.AnswerToSecurityQuestion);

                if (registrationData.IsRegistred)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, registrationData.UserName),
                        new Claim(ClaimTypes.Email, registrationData.Email)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties).Wait();

                    _authService.SetUserStatus(registrationData.UserName, true);

                    return RedirectToAction("Welcome", "Home", new { username = registrationData.UserName });
                }

                ModelState.AddModelError(string.Empty, registrationData.ErrorMessage);
            }

            var securityQuestions = SecurityQuestions.Questions.Select(q => new SelectListItem
            {
                Text = q,
                Value = q
            }).ToList();
            ViewBag.SecurityQuestions = securityQuestions;

            return View(model);
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            var forgotPasswordModel = new ForgotPasswordViewModel();
            return View(forgotPasswordModel);
        }

        /// <summary>
        /// Handles the "Forgot Password" process by verifying the user's identity and allowing them 
        /// to reset their password if valid. Provides appropriate feedback for errors or mismatches.
        /// </summary>
        /// <param name="model"> The <see cref="ForgotPasswordViewModel"/> containing the user's input, including username, email, security question answer, and new password details. </param>
        /// <returns>
        /// Returns the Forgot Password view with validation feedback on failure, or redirects to 
        /// the Home page if the password is successfully changed.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (string.IsNullOrEmpty(model.Password))
            {
                var userExist = _authService.IsUserExist(model.Username, model.Email);

                if (!userExist)
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "User not found.");
                    return View(model);
                }

                model.UserExists = true;
                model.SecurityQuestion = model.SecurityQuestion;
                return View(model);
            }
            else
            {
                if (!model.Password.Equals(model.ConfirmPassword))
                {
                    ModelState.Clear();
                    ModelState.AddModelError(string.Empty, "Passwords do not match.");
                    return View(model);
                }

                var registrationData = await _authService.TryToChangePassword(model.Username, model.Email, model.Password, model.AnswerToSecurityQuestion);

                if (registrationData.IsPasswordChanged)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.Clear();
                ModelState.AddModelError(string.Empty, registrationData.ErrorMessage);
                return View(model);
            }
        }
    }
}
