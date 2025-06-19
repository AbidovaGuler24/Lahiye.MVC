using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.Core.Helpers.Exictance;
using OnlineLearning.DAL.Context;

namespace Lahiye.Mvc.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult AddToCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<int>>("Cart") ?? new List<int>();
            if (!cart.Contains(id))
                cart.Add(id);

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index", "PaidBook"); // və ya Home
        }

        public IActionResult ViewCart()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<int>>("Cart") ?? new List<int>();

            // ID-lərə əsaslanaraq PaidBook-ları al
            var booksInCart = _context.PaidBooks
                                      .Where(b => cart.Contains(b.Id))
                                      .ToList();

            return View(booksInCart); // Artıq View doğru model alır
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<int>>("Cart") ?? new List<int>();
            cart.Remove(id);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("ViewCart");
        }
    }
}
