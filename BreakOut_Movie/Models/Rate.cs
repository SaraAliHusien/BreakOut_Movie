using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BreakOut_Movie.Models
{
    public class Rate
    {
        [Column(Order = 0), Key, ForeignKey("Movie")]
        public int MovieId { get; set; } = default!;
        [Column(Order = 0), Key, ForeignKey("ApplicationUser")]
        public string UserId { get; set; } = default!;
        [Range(1,5)]
        public byte RateValue { get; set; }= default!;

        public virtual Movie Movie { get; set; } = default!;
        public virtual ApplicationUser User { get; set; } =default!;

    }
}
