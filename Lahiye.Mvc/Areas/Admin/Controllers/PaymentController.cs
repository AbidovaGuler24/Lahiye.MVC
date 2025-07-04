using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.DAL.Context;
using Stripe;
using Stripe.Checkout;

namespace Lahiye.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        public PaymentController(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }
        public IActionResult Index()
        {
          
            var secretKey = _configuration["Stripe:SecretKey"];
            var publishableKey = _configuration["Stripe:PublishableKey"];

         
            ViewBag.PublishableKey = publishableKey;

            return View();
        }
        [HttpPost]
        public IActionResult Checkout(decimal amount, int bookId)
        {
            var domain = $"{Request.Scheme}://{Request.Host}";

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(amount * 100), // qepik cinsindən (100 = 1 AZN)
                            Currency = "azn",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Pullu Kitab Satışı",
                            },
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = domain + $"/Admin/Payment/Success?bookId={bookId}",
                CancelUrl = domain + "/Admin/Payment/Cancel",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Redirect(session.Url);
        }
        public IActionResult Success(int bookId)
        {
            ViewBag.BookId = bookId;
            ViewBag.Message = "Ödəniş uğurla həyata keçirildi!";
            return View();
        }
        public IActionResult Cancel()
        {
            ViewBag.Message = "Ödəniş ləğv edildi.";
            return View();
        }
        public IActionResult DownloadBook(int bookId)
        {
            var book = _context.PaidBooks.FirstOrDefault(b => b.Id == bookId);
            if (book == null || string.IsNullOrEmpty(book.Pdf))
            {
                return NotFound("Kitab tapılmadı.");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", book.Pdf.TrimStart('/'));
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("PDF faylı serverdə tapılmadı.");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/pdf", $"{book.Title}.pdf");
        }
    }
}
