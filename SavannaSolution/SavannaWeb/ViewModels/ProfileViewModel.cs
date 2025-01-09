using Savanna.Data.Data;
using System.ComponentModel.DataAnnotations;

namespace SavannaWeb.ViewModels
{
    public class ProfileViewModel
    {
        public int UserId {  get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [Display(Name = "Username")]
        [StringLength(20, ErrorMessage = "Username can't be longer than 20 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username can only contain letters and numbers.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
