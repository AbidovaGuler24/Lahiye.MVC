using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.BL.Services.Concretes;
using OnlineLearning.Core.ViewModels;

namespace Lahiye.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CafeMenuController : Controller
    {
        private readonly ICafeMenuService _cafeMenuService;
        private readonly IWebHostEnvironment _env;

        public CafeMenuController(ICafeMenuService cafeMenuService, IWebHostEnvironment env)
        {
            _cafeMenuService = cafeMenuService;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            //var items = await _cafeMenuService.GetAllAsync();
            //return View(items);
            var items = await _cafeMenuService.GetAllAsync();
            return View(items ?? new List<CafeMenuItemVM>());
        }
        public async Task<IActionResult> Details(int id)
        {
            var item = await _cafeMenuService.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }

        public IActionResult Create()
        {
            return View();
        }

       

        [HttpPost]
        public async Task<IActionResult> Create(CafeMenuItemVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string wwwroot = _env.WebRootPath;
            
            await _cafeMenuService.AddAsync(model, wwwroot);

            TempData["SuccessMessage"] = "Menyu uğurla əlavə olundu!";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _cafeMenuService.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CafeMenuItemVM model)
        {
            if (id != model.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _cafeMenuService.UpdateAsync(model, _env.WebRootPath);
                return RedirectToAction(nameof(Index));
            }
            return View(model);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _cafeMenuService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
