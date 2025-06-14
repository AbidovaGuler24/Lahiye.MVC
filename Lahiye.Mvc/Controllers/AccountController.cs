using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.Enums;
using OnlineLearning.Core.ViewModels;

namespace Lahiye.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVm)
        {
            if (!ModelState.IsValid)
                return View(registerVm);

            AppUser appUser = new AppUser()
            {
                Name = registerVm.Name,
                Email = registerVm.Email,
                Surname = registerVm.Surname,
                UserName = registerVm.Username,
            };

            var result = await _userManager.CreateAsync(appUser, registerVm.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(registerVm);
            }

            
            var allUsers = await _userManager.Users.ToListAsync();

            string roleToAssign = "User";
            if (allUsers.Count == 1)
                roleToAssign = "Admin";
            else if (allUsers.Count == 2)
                roleToAssign = "Moderator";

           
            if (!await _roleManager.RoleExistsAsync(roleToAssign))
                await _roleManager.CreateAsync(new IdentityRole(roleToAssign));

            await _userManager.AddToRoleAsync(appUser, roleToAssign);

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVm, string? returnUrl)
        {
            if (!ModelState.IsValid)
                return View(loginVm);

            var user = await _userManager.FindByEmailAsync(loginVm.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "İstifadəçi tapılmadı.");
                return View(loginVm);
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password!, loginVm.IsRememberMe, false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Email və ya şifrə yanlışdır.");
            return View(loginVm);
        }

        public async Task<IActionResult> CreateRoles()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await _roleManager.RoleExistsAsync("Moderator"))
                await _roleManager.CreateAsync(new IdentityRole("Moderator"));

            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new IdentityRole("User"));

            return Content("Rollar yaradıldı.");
        }
    }
}
