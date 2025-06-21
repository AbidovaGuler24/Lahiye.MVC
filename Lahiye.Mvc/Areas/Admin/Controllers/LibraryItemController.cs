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

       
        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllAsync();
            return View(list);
        }

        
        public IActionResult Create()
        {
            return View();
        }

       
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Create(LibraryItemAddVM vm)
        {
            if 
             (!ModelState.IsValid) 
                return View(vm);
            await _service.AddAsync(vm, _env.WebRootPath);
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var vm = await _service.GetByIdAsync(id);
            if (vm == null) return NotFound();
            return View(vm);
        }

       
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

      
        public async Task<IActionResult> Delete(int id)
        {

            await _service.DeleteAsync(id);
            return RedirectToAction("Index");
        }

      
    }
}
