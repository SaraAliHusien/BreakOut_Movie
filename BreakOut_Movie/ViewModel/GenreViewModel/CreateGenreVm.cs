using BreakOut_Movie.Setting;
using BreakOut_Movie.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace BreakOut_Movie.ViewModel.GenreViewModel
{
    public class CreateGenreVm
    {

        [UniqeName]
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [AllawedExtention(FileSetting.allowedImageExtention)]
        [MaxFileSize(FileSetting.maxLegthByBytes)]
        public IFormFile Image { get; set; } = default!;
    }
}
