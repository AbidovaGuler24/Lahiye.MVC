using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.ViewModels;

namespace Lahiye.Mvc.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {

            var employees = await _employeeService.GetApprovedAsync();

            var viewModels = employees.Select(e => new EmployeeVM
            {
                Id = e.Id,
                FullName = e.Name,
                Position = e.Position,
                PhotoPath = e.PhotoPath
            }).ToList();

            return View(viewModels);

        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(EmployeeAddVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            string wwwroot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            await _employeeService.AddAsync(vm, wwwroot);

            return RedirectToAction("Index");
        }
    }
}
 