using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.ViewModels;

namespace OnlineLearning.BL.Services.Abstracts
{
    public interface ICafeMenuService
    {
        Task<List<CafeMenuItemVM>> GetAllAsync();
        Task<CafeMenuItemVM> GetByIdAsync(int id);
        Task<List<CafeMenuItemVM>> GetByCategoryAsync(int categoryId);
        Task AddAsync(CafeMenuItemVM menuItemVM, string wwwroot);
        Task UpdateAsync(CafeMenuItemVM menuItemVM, string wwwroot);
        Task DeleteAsync(int id);
    }
}
