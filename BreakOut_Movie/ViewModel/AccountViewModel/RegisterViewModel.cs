using System.ComponentModel.DataAnnotations;

namespace BreakOut_Movie.ViewModel.AccountViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "* User name is required")]
        public string? User_Name { get; set; }
        [Required(ErrorMessage = "* Password is required")]
        [DataType(DataType.Password)]
        public string? UserPassword { get; set; }
        [Required(ErrorMessage = "* Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("UserPassword",ErrorMessage ="Confirm PassWord and paswword do not match"),]
        public string? confirmPassword { get; set; }
    }
}
