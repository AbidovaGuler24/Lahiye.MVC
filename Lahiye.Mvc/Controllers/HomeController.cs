using Microsoft.AspNetCore.Mvc;

namespace Lahiye.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
