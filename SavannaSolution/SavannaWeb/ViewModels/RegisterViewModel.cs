using System.ComponentModel.DataAnnotations;

namespace SavannaWeb.ViewModels
{
    public class RegisterViewModel
    {
        /// <summary>
        /// Gets or Sets the Username for registration.
        /// </summary>
        [Required]
        [Display(Name = "Username")]
        [StringLength(20, ErrorMessage = "Username can't be longer than 20 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username can only contain letters and numbers.")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or Sets the Email for registration.
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets the password for login for registration.
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

        /// <summary>
        /// Gets or Sets the SecurityQuestion.
        /// </summary>
        [Required]
        [Display(Name = "SecurityQuestion")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Choose your security question.")]
        public string SecurityQuestion { get; set; }

        /// <summary>
        /// Gets or Sets the AnswerToSecurityQuestion.
        /// </summary>
        [Required]
        [Display(Name = "AnswerToSecurityQuestion")]
        [StringLength (100, MinimumLength = 1, ErrorMessage = "Enter your answer to the selected security question.")]
        public string AnswerToSecurityQuestion { get; set; }
    }
}
