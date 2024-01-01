using System.ComponentModel.DataAnnotations;

namespace BreakOut_Movie.Models
{
        public class Movie
        {
            public int Id { get; set; }

            [ MaxLength(250)]
            public string Title { get; set; }=string.Empty;
          
            [MaxLength(2500)]
           public string StoryLine { get; set; } = string.Empty;

           public double Year { get; set; } = default!;
        
            public byte[] Poster { get; set; } = default!;
            [MaxLength(250)]
           public string VideoName { get; set; } = string.Empty;
           [MaxLength(500)]
            public string? Cast { get; set; } = string.Empty;
            public byte GenreId { get; set; }
            public virtual Genre Genre { get; set; }=default!;
    

    }
}

