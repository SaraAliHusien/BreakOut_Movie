using BreakOut_Movie.Models;
using BreakOut_Movie.ViewModel.AccountViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BreakOut_Movie.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManger, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManger;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View("Index");
        }
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.redirecet = 1;
            return View("Index");
        }
        public IActionResult Login()
        {

            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel newUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new();
                userModel.UserName = newUser.User_Name;
                userModel.PasswordHash = newUser.UserPassword;

                IdentityResult result = await _userManager.CreateAsync(userModel, userModel.PasswordHash); ;
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(userModel, isPersistent: false);  
                    return RedirectToAction("UserHome", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            ViewBag.registredUser = newUser;
            ViewBag.redirecet = 1;
            return View("Index");
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = await _userManager.FindByNameAsync(loginUser.UserName);
                
                if (userModel != null)
                {
                    bool found = await _userManager.CheckPasswordAsync(userModel, loginUser.Password);
                    if (found)
                    {
                        await _signInManager.SignInAsync(userModel, loginUser.RememberMe);
                        var inRoleAdmin = await _userManager.IsInRoleAsync(userModel, "Admin");
                        var inRoleMainAdmin = await _userManager.IsInRoleAsync(userModel, "MainAdmin");

                        if (inRoleAdmin || inRoleMainAdmin) 
                            return RedirectToAction("Index", "Movie");
                        return RedirectToAction("UserHome", "Home");
                    }
                    ModelState.AddModelError("Password", "password invalid");
                    ViewBag.usermodel = loginUser;
                    return View("Index"); 
                }
                ModelState.AddModelError("UserName", "User Name invalid");

            }
            ViewBag.usermodel = loginUser;
            return View("Index");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        private async Task<IActionResult> AddAdmin(RegisterViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser NewUser = new();
                NewUser.UserName = userModel.User_Name;
                NewUser.PasswordHash = userModel.UserPassword;

                IdentityResult result = await _userManager.CreateAsync(NewUser, NewUser.PasswordHash); ;
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(NewUser, isPersistent: false);
                    await _userManager.AddToRoleAsync(NewUser, "MainAdmin");
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
           
            return View("Index");
        }

       
    }
}
