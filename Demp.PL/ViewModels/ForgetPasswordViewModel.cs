using System.ComponentModel.DataAnnotations;

namespace Demp.PL.ViewModels
{
    public class ForgetPasswordViewModel
    {

        [EmailAddress]
        public string Email { get; set; } = null!;
    }
}
