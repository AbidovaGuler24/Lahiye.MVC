using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.DAL.Repositories.Abstracts
{
    public interface ICafeMenuRepository
    {
        Task<List<CafeMenuItem>> GetAllAsync();
        Task<CafeMenuItem> GetByIdAsync(int id);
        Task AddAsync(CafeMenuItem menuItem);
        Task UpdateAsync(CafeMenuItem menuItem);
        Task DeleteAsync(int id);
        Task<List<CafeMenuItem>> GetByCategoryAsync(int categoryId);
        Task SaveAllChangesAsync();
    }

}
