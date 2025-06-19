using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.ViewModels;

namespace OnlineLearning.BL.Services.Abstracts
{
    public interface IPaidBookService
    {
        Task<List<PaidBookVm>> GetAllAsync();
        Task<PaidBookVm?> GetByIdAsync(int id);
        Task CreateAsync(PaidBookCreateVm vm, string wwwroot);
        Task UpdateAsync(PaidBookUpdateVm vm , string wwwroot);
        Task DeleteAsync(int id);
        Task<List<PaidBookVm>> GetFilteredAsync(string? search, decimal? minPrice, int? minPage);
    }
}
