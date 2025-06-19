using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.BL.Services.Concretes;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.ViewModels;
using OnlineLearning.DAL.Context;

namespace OnlineLearning.MVC.Controllers
{
    [Area("Admin")]
    public class PaidBookController : Controller
    {
        private readonly IPaidBookService _paidBookService;
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;


        public PaidBookController(IPaidBookService paidBookService, IWebHostEnvironment env, AppDbContext context)
        {
            _paidBookService = paidBookService;
            _env = env;
            _context = context;
        }

        // GET: PaidBook
        public async Task<IActionResult> Index(string search, decimal? minPrice, int? minPage)
        {
            var books = await _paidBookService.GetFilteredAsync(search, minPrice, minPage);
            return View(books);
        }

        // GET: PaidBook/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var book = await _paidBookService.GetByIdAsync(id);
            if (book == null) return NotFound();

            return View(book);
        }

        // GET: PaidBook/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaidBook/Create
        [HttpPost]
        public async Task<IActionResult> Create(PaidBookCreateVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            string wwwroot = _env.WebRootPath;
            await _paidBookService.CreateAsync(vm, wwwroot);

            return RedirectToAction(nameof(Index));
        }

        // GET: PaidBook/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _paidBookService.GetByIdAsync(id);
            if (book == null) return NotFound();

            var vm = new PaidBookUpdateVm
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                PageCount = book.PageCount,
                Img = book.Img,
                Pdf = book.Pdf,
                Price = book.Price
            };
            return View(vm);
        }

        // POST: PaidBook/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(PaidBookUpdateVm vm)
        {
            if (!ModelState.IsValid) return View(vm);

            string wwwroot = _env.WebRootPath;
            await _paidBookService.UpdateAsync(vm, wwwroot);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _paidBookService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddToFavoritesAndCart(int bookId)
        {
            var userId = "test-user"; // Əgər login sistemi varsa: User.Identity.Name və ya UserManager ilə al

            // Favoritə əlavə et
            if (!_context.FavoriteBooks.Any(f => f.BookId == bookId && f.UserId == userId))
            {
                _context.FavoriteBooks.Add(new FavoriteBook
                {
                    BookId = bookId,
                    UserId = userId
                });
            }

            // Səbətə əlavə et
            if (!_context.CartItems.Any(c => c.BookId == bookId && c.UserId == userId))
            {
                _context.CartItems.Add(new CartItem
                {
                    BookId = bookId,
                    UserId = userId,
                    Quantity = 1
                });
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
