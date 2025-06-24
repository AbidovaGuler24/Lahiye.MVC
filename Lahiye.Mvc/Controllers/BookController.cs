using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.BL.Services.Concretes;
using OnlineLearning.Core.Enums;
using OnlineLearning.Core.ViewModels;
using OnlineLearning.DAL.Context;

namespace Lahiye.Mvc.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly AppDbContext _context;

        public BookController(IBookService bookService, AppDbContext context)
        {
            _bookService = bookService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
                             .Select(c => c.Name) 
                             .ToListAsync();

            ViewBag.Categories = categories;

            
            var books = await _bookService.GetAllBooksAsync();

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
        

        public async Task<IActionResult> RelatedBooks(int? categoryId, int excludeBookId)
        {
            var relatedBooks = await _bookService.GetBooksByCategoryIdAsync(categoryId, excludeBookId);
            return View(relatedBooks);
        }
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery]string searchTerm)
        {
            var books = await _bookService.GetAllBooksAsync();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                books = books
                    .Where(b => b.Category != null && b.Category.Name.Contains(searchTerm))
                    .ToList();
            }

            return View("Index", books); 
        }
    }
}
