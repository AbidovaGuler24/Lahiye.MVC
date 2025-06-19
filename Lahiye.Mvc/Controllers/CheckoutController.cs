using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineLearning.DAL.Context;
using OnlineLearning.Core.Entities;
using Stripe.Checkout;
using System.Text;
using Stripe;

namespace OnlineLearning.Mvc.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public CheckoutController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        private string ToAsciiOnly(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var sb = new StringBuilder();
            foreach (var ch in input)
            {
                if (ch >= 32 && ch <= 127)
                    sb.Append(ch);
                else
                    sb.Append('?');
            }
            return sb.ToString();
        }

        public IActionResult CreateCheckoutSession(int bookId)
        {
            var book = _context.PaidBooks.FirstOrDefault(x => x.Id == bookId);
            if (book == null) return NotFound();

            var domain = $"{Request.Scheme}://{Request.Host}";

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(book.Price * 100), // AZN olaraq qiymət
                            Currency = "azn",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = ToAsciiOnly(book.Title)
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = domain + $"/Checkout/Success?bookId={book.Id}",
                CancelUrl = domain + "/Checkout/Cancel"
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }

        public IActionResult Success(int bookId)
        {
            var userId = "test-user"; // Əgər ASP.NET Identity varsa, User.Identity.Name və ya User.FindFirst(ClaimTypes.NameIdentifier).Value ilə əvəz et

            // Kitab artıq alınmayıbsa əlavə et
            bool alreadyPurchased = _context.PurchasedBooks.Any(p => p.BookId == bookId && p.UserId == userId);
            if (!alreadyPurchased)
            {
                _context.PurchasedBooks.Add(new PurchasedBook
                {
                    UserId = userId,
                    BookId = bookId,
                    PurchaseDate = DateTime.Now
                });
                _context.SaveChanges();
            }

            return View(); // Success.cshtml view göstərilir
        }

        public IActionResult Cancel()
        {
            return Content("Ödəniş ləğv edildi.");
        }
    }
}
