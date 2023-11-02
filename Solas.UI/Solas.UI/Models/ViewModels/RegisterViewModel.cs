using System.ComponentModel.DataAnnotations;

namespace Solas.UI.Models.ViewModels
{
    public class RegisterViewModel

    {
        [Required]
        [MaxLength(10, ErrorMessage = "Max 10 char")]

        public string? FirstName { get; set; }
        [MaxLength(15, ErrorMessage = "Max 15 char")]

        public string? LastName { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [MaxLength(40, ErrorMessage = "Max 40 char")]
        [EmailAddress(ErrorMessage = "Example: user@ggg.com")]

        public string? Email { get; set; }
        [Required(ErrorMessage = "Please enter Password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Please enter confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Miss Match")]
        public string? ConfirmPassword { get; set; }

        public string? Mobile { get; set; }
    }
}
