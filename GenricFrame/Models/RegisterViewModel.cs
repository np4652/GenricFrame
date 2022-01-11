using System.ComponentModel.DataAnnotations;

namespace GenricFrame.Models
{
    public class RegisterViewModel
    {
        public string RoleName { get; set; }
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and compare password do not match..")]
        public string ConfirmPassword { get; set; }
    }
}
