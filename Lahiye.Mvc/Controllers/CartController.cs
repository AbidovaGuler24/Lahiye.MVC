using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.BL.Services.Concretes;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.Helpers.Exictance;
using OnlineLearning.DAL.Context;
using Stripe;
using Stripe.Checkout;

namespace Lahiye.Mvc.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IPaidBookService _paidBookService;
        private readonly IPaymentService _paymentService;
        public CartController(ICartService cartService, IPaidBookService paidBookService, IPaymentService paymentService)
        {
            _cartService = cartService;
            _paidBookService = paidBookService;
            _paymentService = paymentService; 
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int bookId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            bool added = await _cartService.AddToCartAsync(userId, bookId);

            if (added)
            {
                TempData["SuccessMessage"] = "Kitab səbətə əlavə olundu.";
            }
            else
            {
                TempData["InfoMessage"] = "Kitab artıq səbətdə mövcuddur.";
            }

            return RedirectToAction("Index", "PaidBook");
        }
        public  async Task<IActionResult> ViewCartAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cartItems = await _cartService.GetCartItemsAsync(userId);
            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int bookId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            await _cartService.RemoveFromCartAsync(userId, bookId);
            return RedirectToAction("ViewCart");
        }
        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            await _cartService.ClearCartAsync(userId);
            return RedirectToAction("ViewCart");
        }
       
        [HttpPost]
        public async Task<IActionResult> AddSingleItem(int bookId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Json(new { success = false, message = "Zəhmət olmasa, daxil olun." });
            }

            await _cartService.AddSingleItemAsync(userId, bookId);
            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveSingleItem(int bookId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Json(new { success = false, message = "Zəhmət olmasa, daxil olun." });
            }

            await _cartService.RemoveSingleItemAsync(userId, bookId);
            return Json(new { success = true });
        }
        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cartItems = await _cartService.GetCartItemsAsync(userId);

            if (!cartItems.Any())
            {
                TempData["ErrorMessage"] = "Səbətiniz boşdur.";
                return RedirectToAction("ViewCart");
            }

            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var userName = User.Identity?.Name;
            var userSurname = ""; // Əgər soyad varsa, əlavə et.

            var basketItems = cartItems.Select(c => new BasketItem
            {
                Count = c.Quantity,
                Price = c.PaidBook.Price
            }).ToList();

            var paymentIntentId = _paymentService.CreatePayment(basketItems, userEmail, userId, userName, userSurname);

            TempData["PaymentIntentId"] = paymentIntentId;
            return RedirectToAction("PayPage", "Payment");
        }
        [HttpPost]
        public async Task<IActionResult> CreateCheckoutSession()
        {
            var cartItems = await _cartService.GetCartItemsAsync(User.Identity.Name);
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },

                LineItems = cartItems.Select(item => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "usd",
                        UnitAmount = (long)(item.PaidBook.Price * 100),
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.PaidBook.Title,
                        },
                    },
                    Quantity = item.Quantity,
                }).ToList(),
                Mode = "payment",
                SuccessUrl = Url.Action("Success", "Checkout", null, Request.Scheme) + "?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = Url.Action("Cancel", "Checkout", null, Request.Scheme),
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);

            return Json(new { id = session.Id });
        }


    }
}
