using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.ViewModels;

namespace Lahiye.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IWebHostEnvironment _environment;

        public AuthorController(IAuthorService authorService,IWebHostEnvironment environment)
        {
            _authorService = authorService;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            
            var authors = await _authorService.GetAllAuthorsAsync();
            return View(authors);
        }

        public async Task<IActionResult> Details(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null) return NotFound();

            return View(author);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> Create(AddAuthorVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string wwwroot = _environment.WebRootPath;
            var result = await _authorService.AddAuthorAsync(model, wwwroot);
            TempData["SuccessMessage"] = result;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null) return NotFound();

            var vm = new UpdateAuthorVm
            {
                Id = author.Id,
                FullName = author.FullName,
                Biography = author.Biography
            };

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UpdateAuthorVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                string wwwroot = _environment.WebRootPath;
                await _authorService.UpdateAuthorAsync(model,wwwroot);
                TempData["SuccessMessage"] = "Author yeniləndi.";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

      

        public async Task<IActionResult> Delete(int id)
        {
            var book = await _authorService.GetAuthorByIdAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _authorService.DeleteAuthorAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
