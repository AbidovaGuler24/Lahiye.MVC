using System.Net.NetworkInformation;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.DAL.Context;
using OnlineLearning.Core.ViewModels;
using OnlineLearning.Core.Helpers.Exictance;
using OnlineLearning.BL.Services.Concretes;
using System.IO;
namespace Lahiye.Mvc.Controllers
{
    public class PaidBookController : Controller
    {
        private readonly IPaidBookService _paidBookService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;

        public PaidBookController(IPaidBookService paidBookService, IWebHostEnvironment webHostEnvironment, AppDbContext context)
        {
            _paidBookService = paidBookService;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
                            .Select(c => c.Name)
                            .ToListAsync();

            ViewBag.Categories = categories;
            var books = await _paidBookService.GetAllAsync();
            return View(books);

        }


        //public async Task<IActionResult> ReadOnline(int id)
        //{
        //    var book = await _paidBookService.GetByIdAsync(id);
        //    if (book == null || string.IsNullOrEmpty(book.Pdf))
        //        return NotFound();

        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", book.Pdf);
        //    if (!System.IO.File.Exists(filePath))
        //        return NotFound();

        //    var memory = new MemoryStream();
        //    using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        //    {
        //        await stream.CopyToAsync(memory);
        //    }
        //    memory.Position = 0;

        //    return File(memory, "application/pdf");
        //}

        //public IActionResult Reader(int id)
        //{
        //    ViewBag.BookId = id;
        //    return View();
        //}


        public IActionResult GetFirst10Pages(string pdfFileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", pdfFileName);
            string tempFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "temp");

            if (!System.IO.File.Exists(filePath))
                return NotFound("PDF dosyası bulunamadı.");

            if (!Directory.Exists(tempFolder))
            {
                Directory.CreateDirectory(tempFolder);
            }

            string tempFileName = $"first10pages_{Path.GetFileNameWithoutExtension(pdfFileName)}_{DateTime.Now.Ticks}.pdf";
            string tempFilePath = Path.Combine(tempFolder, tempFileName);

            using (PdfDocument inputDocument = PdfReader.Open(filePath, PdfDocumentOpenMode.Import))
            {
                PdfDocument outputDocument = new PdfDocument();

                int pageCount = Math.Min(10, inputDocument.PageCount);
                for (int i = 0; i < pageCount; i++)
                {
                    outputDocument.AddPage(inputDocument.Pages[i]);
                }

                outputDocument.Save(tempFilePath);
            }

            var bytes = System.IO.File.ReadAllBytes(tempFilePath);
            return File(bytes, "application/pdf");
        }

        public async Task<IActionResult> ReadBook(int id)
        {
            var book = await _paidBookService.GetByIdAsync(id);
            if (book == null || string.IsNullOrEmpty(book.Pdf))
                return NotFound();

            // TODO: Burada real istifadəçi ID-sini ASP.NET Identity'dən al
            var userId = "test-user";

            bool isPaid = _context.PurchasedBooks.Any(x => x.BookId == id && x.UserId == userId);

            var viewModel = new PaidBookVm
            {
                Id = book.Id,
                Title = book.Title,
                Pdf = book.Pdf,
                IsPaid = isPaid
            };

            return View(viewModel);
        }
        public IActionResult AddToCart(int id)
        {
            List<int> cart = HttpContext.Session.GetObjectFromJson<List<int>>("Cart") ?? new List<int>();

            if (!cart.Contains(id))
            {
                cart.Add(id);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }

            return RedirectToAction("Cart");
        }

        public async Task<IActionResult> Cart()
        {
            List<int> cart = HttpContext.Session.GetObjectFromJson<List<int>>("Cart") ?? new List<int>();
            var books = await _paidBookService.GetAllAsync();

            var cartBooks = books.Where(b => cart.Contains(b.Id)).ToList();

            return View(cartBooks);
        }

        public IActionResult Checkout()
        {
            return View();
        }
        public async Task<IActionResult> RelatedBooks(int? categoryId, int excludeBookId)
        {
            var relatedBooks = await _paidBookService.GetBooksByCategoryIdAsync(categoryId, excludeBookId);
            return View(relatedBooks);
        }
        public async Task<IActionResult> Details(int id)
        {
            var book = await _paidBookService.GetByIdAsync(id);
            if (book == null) return NotFound();

            var relatedBooks = await _paidBookService.GetBooksByCategoryIdAsync(book.CategoryId, book.Id);

            
            ViewBag.RelatedBooks = relatedBooks ?? new List<PaidBookVm>();

            var vm = new PaidBookVm
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                PageCount = book.PageCount,
                Img = book.Img,
                Pdf = book.Pdf,
                Price = book.Price,
                Category = book.Category,
                 RelatedBooks = relatedBooks
            };

            return View(vm);
        }
        public async Task<IActionResult> Search([FromQuery] string searchTerm)
        {
            var books = await _paidBookService.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                books = books
                    .Where(b => b.Category != null && b.Category.Name.Contains(searchTerm))
                    .ToList();
            }

            return View(books);
        }
       
    }
}