using System.ComponentModel.DataAnnotations;

namespace SavannaWeb.ViewModels
{
    public class ForgotPasswordViewModel
    {
        /// <summary>
        /// Gets or Sets the Username.
        /// </summary>
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or Sets the Email.
        /// </summary>
        [Required]
        [Display (Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets the SecurityQuestion.
        /// </summary>
        [Required]
        [Display(Name = "SecurityQuestion")]
        public string SecurityQuestion { get; set; }

        /// <summary>
        /// Gets or Sets the AnswerToSecurityQuestion.
        /// </summary>
        [Required]
        [Display(Name = "AnswerToSecurityQuestion")]
        public string AnswerToSecurityQuestion {  get; set; }

        /// <summary>
        /// Gets or Sets the password for login.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 9 characters long.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[\W_]).*$", ErrorMessage = "Password must contains at least one uppercase letter and one special character.")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets the confirmation password for registration.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not mathc.")]
        public string ConfirmPassword { get; set; }

        public bool UserExists {  get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether the user wants to be remembered.
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
