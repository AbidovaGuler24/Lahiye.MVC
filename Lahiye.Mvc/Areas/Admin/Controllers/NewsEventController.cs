using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.BL.Services.Concretes;
using OnlineLearning.Core.ViewModels;

namespace Lahiye.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NewsEventController : Controller
    {
        private readonly INewsEventService _newsEventService;
        private readonly IWebHostEnvironment _env;

        public NewsEventController(INewsEventService newsEventService, IWebHostEnvironment env)
        {
            _newsEventService = newsEventService;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var events = await _newsEventService.GetAllAsync();
            return View(events);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
      
        public async Task<IActionResult> Create(NewsEventVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            string rootPath = _env.WebRootPath;
            await _newsEventService.AddAsync(vm, rootPath);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var newsEvent = await _newsEventService.GetByIdAsync(id);
            if (newsEvent == null) return NotFound();

            var updateVm = new UpdateNewsEventVm
            {
                Id = newsEvent.Id,
                Title = newsEvent.Title,
                Description = newsEvent.Description,
                Date = newsEvent.Date,
                ExistingImagePath = newsEvent.ImagePath
            };

            return View(updateVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateNewsEventVm vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            string rootPath = _env.WebRootPath;

           
            var newsEventVm = new NewsEventVm
            {
                Id = vm.Id,
                Title = vm.Title,
                Description = vm.Description,
                Date = vm.Date,
                ImagePath = vm.ExistingImagePath,
                ImageFile = vm.ImageFile,

            };

            await _newsEventService.UpdateAsync(newsEventVm, rootPath);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Details(int id)
        {
            var newsEvent = await _newsEventService.GetByIdAsync(id);
            if (newsEvent == null) return NotFound();
            return View(newsEvent);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _newsEventService.GetByIdAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _newsEventService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
