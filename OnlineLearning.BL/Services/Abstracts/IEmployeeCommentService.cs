using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.BL.Services.Abstracts
{
    public interface IEmployeeCommentService
    {
        Task AddAsync(EmployeeComment comment);
        Task<List<EmployeeComment>> GetByEmployeeIdAsync(int employeeId);
    }
}
