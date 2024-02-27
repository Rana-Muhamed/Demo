using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Password is required")]
        //[MinLength(5, ErrorMessage = "Minimum Password Length is 5′′)]
        [DataType(DataType.Password)]

        public string NewPassword { get; set; }


        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("NewPassword", ErrorMessage = "Confirm Password does not match Password")]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }

        //public string Email { get; set; }
        //public string Token { get; set; }
    }
}
