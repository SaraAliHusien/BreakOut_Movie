
using BreakOut_Movie.Setting;
using System.ComponentModel.DataAnnotations;

namespace BreakOut_Movie.ValidationAttributes
{
    public class MaxFileSizeAttribute:ValidationAttribute
    { 
        private readonly int _maxFileSize;
        private readonly int _maxFileSizebyMG;
        public MaxFileSizeAttribute( int maxSize)
        {
            _maxFileSize = maxSize;
            _maxFileSizebyMG = FileSetting.maxLegthByMega;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file is not null)
            {
                if ((file.Length > _maxFileSize))
                {
                    return new ValidationResult($"Maximum allowed size is {_maxFileSizebyMG}MB!");
                }
            }
            return ValidationResult.Success;
        }
    }
}
