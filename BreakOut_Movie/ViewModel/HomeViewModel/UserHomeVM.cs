namespace BreakOut_Movie.ViewModel.HomeViewModel
{
    public class UserHomeVM
    {
        public HomeIndexVM Movie { get; set; } = default!;
        public int userRate { get; set; }
        public bool IsFavorite { get; set; }
    }
}
