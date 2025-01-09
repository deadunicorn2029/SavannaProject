using System.ComponentModel.DataAnnotations;

namespace SavannaWeb.ViewModels
{
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or Sets the Username for login.
        /// </summary>
        [Required]
        [Display(Name = "Username or Email")]
        public string UsernameOrEmail { get; set; }

        /// <summary>
        /// Gets or Sets the Password for login.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets a value indicating whether the user wants to be remembered.
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
