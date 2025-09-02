using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.ViewModels;

namespace Lahiye.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPaidBookService _paidBookService;

        
        public HomeController(IPaidBookService paidBookService)
        {
            _paidBookService = paidBookService;
        }

       
        public async Task<IActionResult> Index()
        {
            List<PaidBookVm> allBooks = await _paidBookService.GetAllAsync();

            return View(allBooks);
        }
    }
}
