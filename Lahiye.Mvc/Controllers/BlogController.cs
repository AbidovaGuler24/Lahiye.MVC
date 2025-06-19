using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;
using System.Threading.Tasks;

namespace Lahiye.Mvc.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> Blog()
        {
            var blogs = await _blogService.GetAllBlogsAsync(); 
            return View(blogs); 
        }
        public async Task<IActionResult> Details(int id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);
            if (blog == null) return NotFound();

            return View(blog);
        }
    }
}
