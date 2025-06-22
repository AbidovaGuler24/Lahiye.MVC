using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.BL.Services.Concretes;
using OnlineLearning.Core.ViewModels;

namespace Lahiye.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IWebHostEnvironment _env;
        private readonly ICategoryService _categoryService;
        public BookController(IBookService bookService, IWebHostEnvironment env, ICategoryService categoryService)
        {
            _bookService = bookService;
            _env = env;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {

            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        public async Task<IActionResult> Create()
        {

            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories;    

            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(AddBookVm model)
        {
            if (!ModelState.IsValid) return View(model);

            string wwwroot = _env.WebRootPath;


            await _bookService.AddBookAsync(model, wwwroot);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
           
            if (book == null) return NotFound();

            var categories = await _categoryService.GetAllCategoriesAsync(); 
            ViewBag.Categories = categories;


            var updateVm = new UpdateBookVm
            {
                Id = id,
                Title = book.Title,
                Description = book.Description,
                PageCount = book.PageCount,
               
                Img = book.Img,
                Pdf = book.Pdf,
                 
            };
            
            return View(updateVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateBookVm model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                ViewBag.Categories = categories;
                return View(model);
            }

            string wwwroot = _env.WebRootPath;
            await _bookService.UpdateBookAsync(model, wwwroot);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return RedirectToAction(nameof(Index));
        }





    }
}
