using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.Helpers.Exictance;
using OnlineLearning.DAL.Context;

namespace Lahiye.Mvc.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
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
    }
}
