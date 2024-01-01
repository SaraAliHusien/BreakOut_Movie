using BreakOut_Movie.Setting;
using BreakOut_Movie.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace BreakOut_Movie.ViewModel.MovieViewModel
{
    public class CreateFormVM : MovieForm
    {

        [Display(Name = "Select poster")]
        [AllawedExtention(FileSetting.allowedImageExtention)]
        [MaxFileSize(FileSetting.maxLegthByBytes)]
        public IFormFile Poster { get; set; } = default!;

        [Display(Name = "Select Video")]
        [AllawedExtention(FileSetting.allowedVideoExtention)]
        public IFormFile Video { get; set; } = default!;

    }
}
