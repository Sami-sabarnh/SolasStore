using System.ComponentModel.DataAnnotations;

namespace Solas.UI.Models.ViewModels
{
    public class LoginViewModel

    {
       

        [Required(ErrorMessage = "Please enter your email")]
        [MaxLength(40, ErrorMessage = "Max 40 char")]
        [EmailAddress(ErrorMessage = "Example: user@ggg.com")]

        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
       

    }
}
