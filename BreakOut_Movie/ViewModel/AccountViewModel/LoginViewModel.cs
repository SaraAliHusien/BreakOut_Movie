using System.ComponentModel.DataAnnotations;

namespace BreakOut_Movie.ViewModel.AccountViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "* User name is required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "* Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
