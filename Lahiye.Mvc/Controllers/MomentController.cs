using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.ViewModels;

namespace Lahiye.Mvc.Controllers
{
    public class MomentController : Controller
    {
        private readonly IMomentService _momentService;
        private readonly ICommentService _commentService;

        public MomentController(IMomentService momentService, ICommentService commentService)
        {
            _momentService = momentService;
            _commentService = commentService;
        }

        // /PublicMoment
        public async Task<IActionResult> Index()
        {
            var moments = await _momentService.GetAllAsync();
            return View(moments);
        }

        // /PublicMoment/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var moment = await _momentService.GetByIdAsync(id);
            if (moment == null) return NotFound();

            var vm = new MomentViewModel
            {
                Title = moment.Title,
                Description = moment.Description,
                ImagePath = moment.ImagePath,
                Id = moment.Id,
                Comments = moment.Comments.Select(x=>new CommentViewModel()
                {
                    MomentId = moment.Id,
                    Content = x.Content,
                    UserName = x.UserName,
                }).ToList(),

            };
            ViewBag.CommentVm = new CommentViewModel();
            

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(MomentViewModel vm,int id)
        {
            //if (!ModelState.IsValid)
            //{
                
            //    return View("Details", vm);
            //}
           
            var viewModel = new CommentViewModel
            {
                MomentId = id,
                Content = vm.Comment.Content,
                UserName = vm.Comment.UserName,
                CreatedAt = DateTime.Now,
            };

            await _commentService.AddCommentAsync(viewModel);
            return RedirectToAction("Details", new { id = vm.Id });
        }
    }
}
