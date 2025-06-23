using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.DAL.Repositories.Abstracts
{
    public interface IPaidBookRepository
    {
        Task<List<PaidBook>> GetAllAsync();
        Task<PaidBook?> GetByIdAsync(int id);
        Task CreateAsync(PaidBook book);
        Task UpdateAsync(PaidBook book);
        Task DeleteAsync(PaidBook book);
        Task<int> SaveAllChangesAsync();
        Task<List<PaidBook>> GetPaidBooksByCategoryIdAsync(int? categoryId, int excludeBookId);
    }
}
