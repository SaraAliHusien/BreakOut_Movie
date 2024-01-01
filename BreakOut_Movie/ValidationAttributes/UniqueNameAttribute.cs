using BreakOut_Movie.Models;
using System.ComponentModel.DataAnnotations;

namespace BreakOut_Movie.ValidationAttributes
{
    public class UniqeNameAttribute : ValidationAttribute
    {
     
        protected override ValidationResult? IsValid(object? value,
            ValidationContext validationContext)
        {
          var  _context= (BreakOut_DbContext)validationContext.GetService(typeof(BreakOut_DbContext))!;
            if (value is not null)
            {
                if (_context.Genres.Any(a => a.Name.ToLower() == value!.ToString()!.ToLower()))
                {
                    return new ValidationResult("This Genre is exists");
                }
            }
            
            return ValidationResult.Success;
        }
    }
}
