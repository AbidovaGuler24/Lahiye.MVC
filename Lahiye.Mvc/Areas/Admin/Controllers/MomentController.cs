using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.ViewModels;
using System.Threading.Tasks;

namespace OnlineLearning.Web.Controllers.Admin
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MomentController : Controller
    {
        private readonly IMomentService _momentService;
        private readonly IWebHostEnvironment _env;

        public MomentController(IMomentService momentService, IWebHostEnvironment env)
        {
            _momentService = momentService;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var moments = await _momentService.GetAllAsync();
            return View(moments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MomentCreateViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            await _momentService.AddMomentAsync(vm, _env.WebRootPath);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var moment = await _momentService.GetByIdAsync(id);
            if (moment == null)
                return NotFound();

            var vm = new MomentEditViewModel
            {
                Title = moment.Title,
                Description = moment.Description,
                ExistingImagePath = moment.ImagePath
               
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, MomentEditViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            try
            {
                await _momentService.UpdateMomentAsync(id, vm, _env.WebRootPath);
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _momentService.DeleteMomentAsync(id, _env.WebRootPath);
            }
            catch (System.Exception ex)
            {
                // Hata idarəsi
                return BadRequest(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
