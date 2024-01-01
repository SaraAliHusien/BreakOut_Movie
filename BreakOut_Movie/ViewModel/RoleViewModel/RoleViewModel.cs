using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BreakOut_Movie.ViewModel.RoleViewModel
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; } = string.Empty;
    }
}
