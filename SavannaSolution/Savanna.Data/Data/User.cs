using System.ComponentModel.DataAnnotations;

namespace Savanna.Data.Data
{
    public class User
    {
        /// <summary>
        /// Unique identifier for the user.
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// Username for the user account.
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Email addres associated with the user account.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// User's password.
        /// </summary>
        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Security question for the user account.
        /// </summary>
        [Required]
        public string SecurityQuestion { get; set; }

        /// <summary>
        /// Answer to security question for the user account.
        /// </summary>
        [Required]
        public string AnswerToSecurityQuestion { get; set; }

        /// <summary>
        /// Indicates whether the user is currently online.
        /// </summary>
        public bool IsOnline { get; set; }
    }
}
