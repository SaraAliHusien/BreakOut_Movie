
using BreakOut_Movie.ViewModel.AccountViewModel;

namespace BreakOut_Movie.ViewModel.AdminViewModel
{
    public class AdminViewModel
    {
        public RegisterViewModel NewAdmin { get; set; } = default!;
        public PromotionUserVM promotion { get; set; } = default!;

    }
}
