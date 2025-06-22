using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.BL.Services.Concretes;
using OnlineLearning.Core.Enums;
using OnlineLearning.Core.ViewModels;

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
            //if (genre.HasValue)
            //{
            //    books = books.Where(b => b.Genre == genre.Value).ToList();
            //}

           
            ViewBag.Genres = Enum.GetValues(typeof(BookGenre)).Cast<BookGenre>().ToList();
            ViewBag.SelectedGenre = genre;

            return View(books);
        }


        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();

            var relatedBooks = await _bookService.GetBooksByCategoryIdAsync(book.CategoryId, book.Id);
            ViewBag.RelatedBooks = relatedBooks;

            return View(book);
        }
        public async Task<IActionResult> Search(string searchTerm)
        {
            var list = await _bookService.GetAllBooksAsync();

            ViewBag.Genres = new List<BookGenre>()

            {
                BookGenre.Bioqrafiya,
            };

            //if (!string.IsNullOrEmpty(searchTerm))
            //{
                

            //    list = list.Where(x =>
            //        x.Genre.ToString().ToLower().Contains(searchTerm)
            //    ).ToList();
            //}

            return View(list);
        }

        public async Task<IActionResult> RelatedBooks(int? categoryId, int excludeBookId)
        {
            var relatedBooks = await _bookService.GetBooksByCategoryIdAsync(categoryId, excludeBookId);
            return View(relatedBooks);
        }

    }
}
