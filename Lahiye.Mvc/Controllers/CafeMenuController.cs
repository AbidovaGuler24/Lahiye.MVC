using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.ViewModels;

namespace Lahiye.Mvc.Controllers
{
    public class CafeMenuController : Controller
    {
        private readonly ICafeMenuService _cafeMenuService;
        public CafeMenuController(ICafeMenuService service)
        {
         _cafeMenuService = service;   
        }
        public async Task<IActionResult> Index()
        {
            var data = await _cafeMenuService.GetAllAsync();
           

            return View(data);
        }
    }
}
