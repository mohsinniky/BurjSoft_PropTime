using System.ComponentModel.DataAnnotations;

namespace IdentityFramework.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email Id is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Password Id is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!; //Null forgiving operator , way to silence compiler warnings
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
