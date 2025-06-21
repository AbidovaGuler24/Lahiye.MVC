using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.ViewModels;
using OnlineLearning.Core.Helpers.Exictance;
using System.Threading.Tasks;

namespace OnlineLearning.Web.Controllers
{
    [Area("Admin")]
    public class LibraryItemController : Controller
    {
        private readonly ILibraryItemService _service;
        private readonly IWebHostEnvironment _env;

        public LibraryItemController(ILibraryItemService service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
        }

        // GET: LibraryItem
        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllAsync();
            return View(list);
        }

        // GET: LibraryItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LibraryItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LibraryItemAddVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _service.AddAsync(vm, _env.WebRootPath);
            return RedirectToAction("Index");
        }

        // GET: LibraryItem/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _service.GetByIdAsync(id);
            if (vm == null) return NotFound();
            return View(vm);
        }

        // POST: LibraryItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LibraryItemUpdateVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            await _service.UpdateAsync(vm, _env.WebRootPath);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var vm = await _service.GetByIdAsync(id);
            if (vm == null)
                return NotFound();

            return View(vm);
        }

        // GET: LibraryItem/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }

      
    }
}
