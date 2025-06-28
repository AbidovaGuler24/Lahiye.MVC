using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineLearning.DAL.Context;
using OnlineLearning.Core.Entities;
using Stripe.Checkout;
using System.Text;
using Stripe;
using OnlineLearning.BL.Services.Abstracts;

namespace OnlineLearning.Mvc.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ICartService cartService;

        public CheckoutController(AppDbContext context, IConfiguration configuration, ICartService cartService)
        {
            _context = context;
            _configuration = configuration;

            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            this.cartService = cartService;
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

        public IActionResult CreateCheckoutSessionBasket(List<int> bookIds)
        {
            // Seçilən kitabların məlumatlarını bazadan çək
            var books = _context.PaidBooks.Where(b => bookIds.Contains(b.Id)).ToList();
            if (books == null || books.Count == 0) return NotFound();

            var domain = $"{Request.Scheme}://{Request.Host}";

            var lineItems = new List<SessionLineItemOptions>();

            foreach (var book in books)
            {
                lineItems.Add(new SessionLineItemOptions
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
                });
            }

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = domain + $"/Checkout/SuccessBasket?bookIds={string.Join(",", bookIds)}",
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

        [HttpGet]
        public IActionResult SuccessBasket(string bookIds)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(bookIds))
            {
                return BadRequest("No book IDs provided.");
            }

            // "15,14,13,16" -> List<int> çevirmək
            var ids = bookIds.Split(',')
                             .Select(idStr => {
                                 bool ok = int.TryParse(idStr, out int id);
                                 return (ok, id);
                             })
                             .Where(x => x.ok)
                             .Select(x => x.id)
                             .ToList();

            if (!ids.Any())
            {
                return BadRequest("No valid book IDs provided.");
            }

            var alreadyPurchasedIds = _context.PurchasedBooks
                .Where(p => p.UserId == userId && ids.Contains(p.BookId))
                .Select(p => p.BookId)
                .ToList();

            var newPurchases = ids
                .Where(id => !alreadyPurchasedIds.Contains(id))
                .Select(id => new PurchasedBook
                {
                    UserId = userId,
                    BookId = id,
                    PurchaseDate = DateTime.Now
                }).ToList();

            if (newPurchases.Any())
            {
                _context.PurchasedBooks.AddRange(newPurchases);
                _context.SaveChanges();
            }
            var result = cartService.ClearCartAsync(userId);

            return View(); // SuccessBasket.cshtml və ya Success.cshtml
        }

        public IActionResult Cancel()
        {
            return Content("Ödəniş ləğv edildi.");
        }
    }
}
