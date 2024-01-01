using Microsoft.AspNetCore.Identity;

namespace BreakOut_Movie.Models.Repo.Absrtraction
{
    public interface IRoleRepo
    {
        public List<IdentityRole> AllRoles();
        public Task<IdentityResult> AddRoleAsync(IdentityRole role);
    }
}
