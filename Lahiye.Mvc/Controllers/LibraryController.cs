using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.Entities;

namespace Lahiye.Mvc.Controllers
{
    [Authorize]
    public class LibraryController : Controller
    {
        private readonly IPurchasedBookService _purchasedBookService;
        private readonly UserManager<AppUser> _userManager;

        public LibraryController(IPurchasedBookService purchasedBookService, UserManager<AppUser> userManager)
        {
            _purchasedBookService = purchasedBookService;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<IActionResult> Buy(int bookId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

           
            if (await _purchasedBookService.IsBookPurchasedAsync(user.Id, bookId))
            {
                TempData["Error"] = "Siz artıq bu kitabı almışsınız.";
                return RedirectToAction("Details", new { id = bookId });
            }

            await _purchasedBookService.AddPurchasedBookAsync(user.Id, bookId);

            TempData["Success"] = "Kitab uğurla alındı!";
            return RedirectToAction("MyBooks");
        }

        public async Task<IActionResult> MyBooks()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var purchasedBooks = await _purchasedBookService.GetPurchasedBooksByUserIdAsync(user.Id);
            return View(purchasedBooks);
        }
    }
}

