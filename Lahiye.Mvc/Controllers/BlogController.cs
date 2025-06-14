using Microsoft.AspNetCore.Mvc;

namespace Lahiye.Mvc.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Blog()
        {
            return View();
        }
    }
}
