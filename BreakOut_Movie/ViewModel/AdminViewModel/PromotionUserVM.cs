using System.ComponentModel.DataAnnotations;

namespace BreakOut_Movie.ViewModel.AdminViewModel
{
    public class PromotionUserVM
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = string.Empty;
    }
}
