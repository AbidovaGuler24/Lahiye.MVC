using Microsoft.AspNetCore.Mvc;
using OnlineLearning.DAL.Context;
using OnlineLearning.Core.Helpers.Exictance;

namespace Lahiye.Mvc.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly AppDbContext _context;

        public FavoritesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult AddToFavorites(int id)
        {
            var favorites = HttpContext.Session.GetObjectFromJson<List<int>>("Favorites") ?? new List<int>();
            if (!favorites.Contains(id))
                favorites.Add(id);

            HttpContext.Session.SetObjectAsJson("Favorites", favorites);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ViewFavorites()
        {
            var favorites = HttpContext.Session.GetObjectFromJson<List<int>>("Favorites") ?? new List<int>();
            var books = _context.PaidBooks.Where(b => favorites.Contains(b.Id)).ToList();
            return View(books);  // buradakı books => List<PaidBook>
        }

        public IActionResult RemoveFromFavorites(int id)
        {
            var favorites = HttpContext.Session.GetObjectFromJson<List<int>>("Favorites") ?? new List<int>();
            favorites.Remove(id);
            HttpContext.Session.SetObjectAsJson("Favorites", favorites);
            return RedirectToAction("ViewFavorites");
        }
    }
}

