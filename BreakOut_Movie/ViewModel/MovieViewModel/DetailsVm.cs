using BreakOut_Movie.Models;


namespace BreakOut_Movie.ViewModel.MovieViewModel
{
    public class DetailsVm
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public double Rate { get; set; }

        public string? StoryLine { get; set; }
        public double Year { get; set; }

        public byte[]? Poster { get; set; }


        public virtual Genre? Genre { get; set; }
    }
}
