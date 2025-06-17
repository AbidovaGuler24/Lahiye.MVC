using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.DAL.Repositories.Abstracts
{
    public interface IEmployeeCommentRepository
    {
        Task<EmployeeComment> GetCommentsByEmployeeIdAsync(int employeeId);
        Task AddCommentAsync(EmployeeComment comment);
    }
}
