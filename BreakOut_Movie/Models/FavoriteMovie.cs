using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BreakOut_Movie.Models
{
    public class FavoriteMovie
    {
        [Column(Order = 0), Key, ForeignKey("Movie")]
        public int MovieId { get; set; } = default!;
        [Column(Order = 0), Key, ForeignKey("ApplicationUser")]
        public string UserId { get; set; } = default!;
        public virtual Movie Movie { get; set; } = default!;
        public virtual ApplicationUser User { get; set; } = default!;
    }
}
