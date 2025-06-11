using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.Enums;
using OnlineLearning.Core.ViewModels;

namespace Lahiye.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private static List<User> users = new List<User>();

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (users.Any(u => u.Username == model.Username))
            {
                ModelState.AddModelError("", "Bu istifadəçi adı artıq mövcuddur.");
                return View(model);
            }

            Role assignedRole;
            if (users.Count == 0)
                assignedRole = Role.Admin;
            else if (users.Count == 1)
                assignedRole = Role.Moderator;
            else
                assignedRole = Role.User;

            users.Add(new User
            {
                Id = users.Count + 1,
                Username = model.Username,
                Password = model.Password,
                role = assignedRole
            });

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "İstifadəçi adı və ya şifrə yanlışdır.");
                return View(model);
            }

            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.role.ToString());

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
