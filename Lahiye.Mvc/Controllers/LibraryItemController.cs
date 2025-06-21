using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.ViewModels;
using Stripe;

namespace Lahiye.Mvc.Controllers
{
    public class LibraryItemController : Controller
    {  private readonly ILibraryItemService _libraryItemService;

        public LibraryItemController(ILibraryItemService libraryItemService)
        {
            _libraryItemService= libraryItemService;
        }

       
        public async Task<IActionResult> Index()
        {
            var libraryItem = await _libraryItemService.GetAllAsync() ?? new List<LibraryItemVM>();
            return View(libraryItem);
        }
        public async Task<IActionResult> Search(string searchTerm)
        {
            var list = await _libraryItemService.GetAllAsync();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();

                list = list.Where(x =>
                    x.Title.ToLower().Contains(searchTerm) ||
                    x.Author.ToLower().Contains(searchTerm))
                    .ToList();
            }

            return View(list);
        }

    }
}
