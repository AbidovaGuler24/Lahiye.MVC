using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.BL.Services.Concretes;
using OnlineLearning.Core.Enums;

namespace Lahiye.Mvc.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        public IActionResult Index(BookGenre? genre)
        {
            var books = _bookService.GetAllBooksAsync().Result; 
            if (genre.HasValue)
            {
                books = books.Where(b => b.Genre == genre.Value).ToList();
            }

           
            ViewBag.Genres = Enum.GetValues(typeof(BookGenre)).Cast<BookGenre>().ToList();
            ViewBag.SelectedGenre = genre;

            return View(books);
        }


        public async Task<IActionResult> Details(int id)
        {
            var newsEvent = await _bookService.GetBookByIdAsync(id);
            if (newsEvent == null) return NotFound();

            return View(newsEvent);
        }
        public async Task<IActionResult> Search(string searchTerm)
        {
            var list = await _bookService.GetAllBooksAsync();

            ViewBag.Genres = new List<BookGenre>()

            {
                BookGenre.Bioqrafiya,
            };

            if (!string.IsNullOrEmpty(searchTerm))
            {
                

                list = list.Where(x =>
                    x.Genre.ToString().ToLower().Contains(searchTerm)
                ).ToList();
            }

            return View(list);
        }


    }
}
