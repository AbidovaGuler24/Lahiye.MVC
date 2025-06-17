using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;

namespace Lahiye.Mvc.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }
    }
}
