using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BreakOut_Movie.Models
{
    public class Genre
    {
      
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public byte Id { get; set; }
       
       [Required, MaxLength(100)]
       public string Name { get; set; }=string.Empty;
       [Required]
       public byte[] Image { get; set; } = default!;
    }
}
