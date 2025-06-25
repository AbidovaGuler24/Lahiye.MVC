using Microsoft.AspNetCore.Mvc;
using OnlineLearning.DAL.Context;
using OnlineLearning.Core.Helpers.Exictance;
using OnlineLearning.Core.Entities;
using OnlineLearning.BL.Services.Abstracts;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;

namespace Lahiye.Mvc.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFavoriteBookService _favoriteBookService;
        private readonly UserManager<AppUser> _userManager;
        public FavoritesController(AppDbContext context, IFavoriteBookService favoriteBookService, UserManager<AppUser> userManager)
        {
            _context = context;
            _favoriteBookService = favoriteBookService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorites(int id)
        {
            var user = await _userManager.GetUserAsync(User); // Identity-dən istifadəçi tap
            if (user == null) return RedirectToAction("Login", "Account");

            var result = await _favoriteBookService.AddFavoriteAsync(user.Id, id);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ViewFavorites(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            var favorites =await _favoriteBookService.GetFavoritesAsync(user.Id);
            
            return View(favorites);  // buradakı books => List<PaidBook>
        }

        public async Task<IActionResult> RemoveFromFavorites(int id)
        {
            await _favoriteBookService.RemoveFavoriteAsync(id);
            return RedirectToAction("ViewFavorites", new { email = User.Identity.Name });
        }
    }
}

