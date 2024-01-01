using Microsoft.AspNetCore.Identity;

namespace BreakOut_Movie.ViewModel.RoleViewModel
{
    public class IndexRoleVM
    {
        public RoleViewModel AddRole { get; set; } = default!;

        public List<IdentityRole> Roles { get; set; } = new List<IdentityRole>();
    }
}
