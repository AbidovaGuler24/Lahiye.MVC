using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.ViewModels;
using static OnlineLearning.Core.ViewModels.EmployeeCommentVM;

namespace OnlineLearning.BL.Services.Abstracts
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task AddAsync(EmployeeAddVm vm, string wwwroot);
        Task UpdateAsync(EmployeeUpdateVm vm, string wwwroot);
        Task DeleteAsync(int id);
        Task<List<Employee>> GetApprovedAsync();
    }
}
