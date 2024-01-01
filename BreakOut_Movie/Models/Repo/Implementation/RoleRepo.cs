using BreakOut_Movie.Models.Repo.Absrtraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BreakOut_Movie.Models.Repo.Implementation
{
    public class RoleRepo:IRoleRepo
    {
        private readonly BreakOut_DbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleRepo(BreakOut_DbContext context, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }

        public List<IdentityRole> AllRoles()
        {
            return _context.Roles.OrderByDescending(role=>role.Name).AsNoTracking().ToList();
        }
        public async Task<IdentityResult> AddRoleAsync(IdentityRole role)
        {
           return await _roleManager.CreateAsync(role);
        }

    }
}
