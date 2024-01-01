using BreakOut_Movie.Models;

namespace BreakOut_Movie.ViewModel.HomeViewModel
{
    public class HomeIndexVM
    {
        public Movie MovieNoRate { get; set; } = default!;
        public int TotalRate { get; set; }
    }
}
