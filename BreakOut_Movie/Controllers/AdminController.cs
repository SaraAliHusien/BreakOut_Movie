using BreakOut_Movie.Models;
using BreakOut_Movie.ViewModel.AccountViewModel;
using BreakOut_Movie.ViewModel.AdminViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace BreakOut_Movie.Controllers
{
    [Authorize(Roles = "MainAdmin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly BreakOut_DbContext _context;
        private readonly IToastNotification _toasNotification;

        public AdminController(UserManager<ApplicationUser> userManger, SignInManager<ApplicationUser> signInManager, BreakOut_DbContext context,
            RoleManager<IdentityRole> roleManager, IToastNotification toasNotification)
        {
            _userManager = userManger;
            _signInManager = signInManager;
            _context = context;
            _roleManager = roleManager;
            _toasNotification = toasNotification;
        }
        public IActionResult AdminIndex()
        {
            var usersWithAdminRole = _userManager.GetUsersInRoleAsync("admin").Result.ToList();
            var ids = usersWithAdminRole.Select(u => u.Id).ToList();
            var model = _userManager.GetUsersInRoleAsync("admin").Result.ToList();

            //ViewBag.RedirectedFrom = "Index";
            return View(model);
        }
      
        public IActionResult AddAdmin()
        {
           

            return View("AddAdmin",new AdminViewModel());
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddAdmin(RegisterViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser NewUser = new();
                NewUser.UserName = userModel.User_Name;
                NewUser.PasswordHash = userModel.UserPassword;

                IdentityResult result = await _userManager.CreateAsync(NewUser, NewUser.PasswordHash);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(NewUser, isPersistent: false);
                    await _userManager.AddToRoleAsync(NewUser, "Admin");
                 
                    return RedirectToAction("AdminIndex", "Admin");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            
            return View("AddAdmin", new AdminViewModel() { NewAdmin=userModel ,promotion=new()});
        }
        public async Task<IActionResult> PromotionRealUserAsync(PromotionUserVM user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = await _userManager.FindByNameAsync(user.UserName);
                if (userModel is not null)
                {
                    var inRole=await _userManager.IsInRoleAsync(userModel, "Admin");
                    if (inRole)
                    {
                        ModelState.AddModelError("","This user is Admin already.!");
                        ViewBag.ForPromotion = true;
                        return View("AddAdmin", new AdminViewModel() { NewAdmin = new(), promotion = user });
                    }
                    var result = await _userManager.AddToRoleAsync(userModel, "Admin");
                    if (result.Succeeded)
                    {
                        _toasNotification.AddSuccessToastMessage("Role Added successfully");
                        return RedirectToAction("AdminIndex", "Admin");
                    }
                   
                        foreach (var error in result.Errors)
                            ModelState.AddModelError("", error.Description);

                }
              else
                ModelState.AddModelError("UserName", "User Name invalid");
            }
            ViewBag.ForPromotion = true;
            return View("AddAdmin", new AdminViewModel() { NewAdmin = new(), promotion = user });

        }
    
        public async Task<IActionResult> RemoveAdminRoleAsync(string id)
        {
            if(id is not null)
            {
                ApplicationUser userModel = await _userManager.FindByNameAsync(id);
                IdentityResult result;
                if (userModel is not null) {
                    result= await _userManager.RemoveFromRoleAsync(userModel,"Admin");
                    return result.Succeeded ? Ok(result) : BadRequest();
                       
                }
                return NotFound();
            }
            return BadRequest();

        }
     
        
    }
}
