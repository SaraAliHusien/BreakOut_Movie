using BreakOut_Movie.Setting;
using BreakOut_Movie.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace BreakOut_Movie.ViewModel.GenreViewModel
{
    public class EditGenreVm
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public byte[]? currentImage { get; set; }

        [AllawedExtention(FileSetting.allowedImageExtention)]
        [MaxFileSize(FileSetting.maxLegthByMega)]
        public IFormFile? Image { get; set; } = default!;
    }
}
