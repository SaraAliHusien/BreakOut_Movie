using BreakOut_Movie.Models;
using System.ComponentModel.DataAnnotations;

namespace BreakOut_Movie.ViewModel.MovieViewModel
{
    public class MovieForm
    {

        [MaxLength(250)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2500)]
        public string StoryLine { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Cast { get; set; } = string.Empty;
        public double Year { get; set; } = default!;

        [Display(Name = "Genre")]
        public byte GenreId { get; set; }
        public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();
    }
}
