using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.ViewModels;
using OnlineLearning.DAL.Repositories.Abstracts;

namespace Lahiye.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {



        private readonly IBlogService _blogService;
        private readonly IWebHostEnvironment _env;

        public BlogController(IBlogService blogService, IWebHostEnvironment env)
        {
            _blogService = blogService;
            _env = env;
        }

        
        public async Task<IActionResult> Index()
        {
            var blogs = await _blogService.GetAllBlogsAsync();
            return View(blogs);
        }

        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> Create(AddBlogVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string wwwroot = _env.WebRootPath;
            await _blogService.AddBlogAsync(model, wwwroot);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);
            if (blog == null) return NotFound();

            var updateVm = new UpdateBlogVm
            {
                Id = blog.Id,
                Description = blog.Description
                
            };

            return View(updateVm);
        }

        
        [HttpPost]
        public async Task<IActionResult> Update(UpdateBlogVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string wwwroot = _env.WebRootPath;
            await _blogService.UpdateBlogAsync(model, wwwroot);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);
            if (blog == null) return NotFound();

            return View(blog);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _blogService.DeleteBlogAsync(id);
            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> Details(int id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);
            if (blog == null) return NotFound();

            return View(blog);
        }


    }
}
