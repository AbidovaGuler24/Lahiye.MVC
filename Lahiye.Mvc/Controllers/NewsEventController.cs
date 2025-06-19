using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;
using System.Threading.Tasks;

namespace Lahiye.Mvc.Controllers
{
    public class NewsEventController : Controller
    {
        private readonly INewsEventService _newsEventService;

        public NewsEventController(INewsEventService newsEventService)
        {
            _newsEventService = newsEventService;
        }

        // /NewsEvent
        public async Task<IActionResult> Index ()
        {
            var newsEvents = await _newsEventService.GetAllAsync();
            return View(newsEvents);
        }

        // /NewsEvent/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var newsEvent = await _newsEventService.GetByIdAsync(id);
            if (newsEvent == null) return NotFound();

            return View(newsEvent);
        }
    }
}
