using Microsoft.AspNetCore.Mvc;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.ViewModels;

namespace Lahiye.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(IEmployeeService employeeService, IWebHostEnvironment environment)
        {
            _employeeService = employeeService;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllAsync();
            return View(employees);
        }

        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null) return NotFound();

            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> Create(EmployeeAddVm model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                string wwwroot = _environment.WebRootPath;
                await _employeeService.AddAsync(model, wwwroot);

                TempData["SuccessMessage"] = "İşçi uğurla əlavə edildi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null) return NotFound();

            var vm = new EmployeeUpdateVm
            {
                Id = employee.Id,
                Name = employee.Name,
                Position = employee.Position,
                ExistingPhotoPath = employee.PhotoPath
            };


            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeUpdateVm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                string wwwroot = _environment.WebRootPath;
                await _employeeService.UpdateAsync(model, wwwroot);
                TempData["SuccessMessage"] = "İşçi yeniləndi.";
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }






        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeService.DeleteAsync(id);
            TempData["SuccessMessage"] = "İşçi silindi.";
            return RedirectToAction(nameof(Index));
        }

    }
}
