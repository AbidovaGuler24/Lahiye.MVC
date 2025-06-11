using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.Enums;

namespace Lahiye.Mvc.Controllers
{
    public class AccountController : Controller
    {
        // Sadə yaddaşda istifadəçilər (Demo məqsədi üçün, realda DB)
        private static List<User> users = new List<User>();

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(string username, string password)
        {
            if (users.Any(u => u.Username == username))
            {
                ModelState.AddModelError("", "Bu istifadəçi adı artıq mövcuddur.");
                return View();
            }

            Role assignedRole;

            if (users.Count == 0)
                assignedRole = Role.Admin;         // Birinci istifadəçi Admin olur
            else if (users.Count == 1)
                assignedRole = Role.Moderator;     // İkinci istifadəçi Moderator olur
            else
                assignedRole = Role.User;           // Digərləri User olur

            users.Add(new User
            {
                Id = users.Count + 1,
                Username = username,
                Password = password,
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
        public IActionResult Login(string username, string password)
        {
            var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                ModelState.AddModelError("", "İstifadəçi adı və ya şifrə yanlışdır.");
                return View();
            }

            // Sessiona istifadəçi məlumatını yazırıq
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