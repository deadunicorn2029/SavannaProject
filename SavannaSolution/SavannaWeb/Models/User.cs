using System.ComponentModel.DataAnnotations;

namespace SavannaWeb.Models
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
        [StringLength(maximumLength: 64, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Security question for the user account.
        /// </summary>
        [Required]
        public string SequrityQuestion { get; set; }

        /// <summary>
        /// Answer to security question for the user account.
        /// </summary>
        [Required]
        public string AnswerToSeqcurityQuestion { get; set; }
    }
}
