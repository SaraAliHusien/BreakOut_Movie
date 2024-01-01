using System.ComponentModel.DataAnnotations;

namespace BreakOut_Movie.ValidationAttributes
{
    public class AllawedExtentionAttribute : ValidationAttribute
    {
        private readonly string _allowedExtention;
        public AllawedExtentionAttribute(string allowedExtention)
        {
            _allowedExtention = allowedExtention;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file is not null)
            {
                var FileExtention = Path.GetExtension(file.FileName);
                if (!(_allowedExtention.Split(',').Contains(FileExtention, StringComparer.OrdinalIgnoreCase)))
                {
                    return new ValidationResult($"only {_allowedExtention} are allowed!");
                }
            }
            return ValidationResult.Success;
        }
    }
}
