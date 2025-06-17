using Microsoft.AspNetCore.Mvc;

namespace Lahiye.Mvc.Controllers
{
    public class NewsEventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
