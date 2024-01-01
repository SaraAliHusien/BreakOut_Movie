using BreakOut_Movie.Models.Repo.Absrtraction;
using BreakOut_Movie.ViewModel.RoleViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace BreakOut_Movie.Controllers
{
    [Authorize(Roles = "MainAdmin")]
    public class RoleController : Controller
    {
        private readonly IRoleRepo _roleRepo;

        private readonly IToastNotification _toasNotification;

        public RoleController(IRoleRepo roleRepo, IToastNotification toasNotification)
        {
            _toasNotification = toasNotification;
            _roleRepo = roleRepo;
        }
        public ActionResult Index() {
            IndexRoleVM indexRoleVM = new IndexRoleVM()
            {
                AddRole = new(),
                Roles = _roleRepo.AllRoles()

            };
            ViewBag.Redirected = false;
            return View("RoleIndex", indexRoleVM);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddRoleAsync(RoleViewModel model) {
            IndexRoleVM roleVM;
            if (!ModelState.IsValid) {

                 roleVM = new IndexRoleVM()
                {
                    Roles = _roleRepo.AllRoles(),
                    AddRole = model,
                };
                return View("RoleIndex", roleVM);
            }
            IdentityRole role = new IdentityRole()
            {
                Name = model.RoleName
            };

            IdentityResult result = await _roleRepo.AddRoleAsync(role);
            if (result.Succeeded)
            {
                _toasNotification.AddSuccessToastMessage("Role Added successfully");
                return  RedirectToAction("Index");
            }
            
                foreach (var item in result.Errors)
                    ModelState.AddModelError("RoleName", item.Description);
             roleVM = new IndexRoleVM()
            {
                Roles = _roleRepo.AllRoles(),
                AddRole=model,
            };
                return View("RoleIndex", roleVM);
            
            
        }
        
     
        
    }
}
