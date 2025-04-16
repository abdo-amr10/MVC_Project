using System.ComponentModel.DataAnnotations;

namespace Demp.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
       

        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password dosen't match Password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
