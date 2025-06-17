using Microsoft.AspNetCore.Mvc;
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
            var employees = await _employeeService.GetAllAsync();
            return View(employees);
        }
    }
}
 