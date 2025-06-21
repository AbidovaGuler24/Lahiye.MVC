using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.BL.Services.Concretes;
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
        public async Task<IActionResult> Search(string searchDay, string searchMonth, string searchYear)
        {
            var list = await _newsEventService.GetAllAsync();

            if (int.TryParse(searchYear, out int year))
            {
                list = list.Where(x => x.Date.Year == year).ToList();
            }

            if (int.TryParse(searchMonth, out int month))
            {
                list = list.Where(x => x.Date.Month == month).ToList();
            }

            if (int.TryParse(searchDay, out int day))
            {
                list = list.Where(x => x.Date.Day == day).ToList();
            }

            return View("Search", list);
        }


    }
}
