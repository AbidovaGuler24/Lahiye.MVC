using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.ViewModels;

namespace OnlineLearning.BL.Services.Abstracts
{
    public interface INewsEventService
    {
        Task<List<NewsEventVm>> GetAllAsync();
        Task<NewsEventVm?> GetByIdAsync(int id);
        Task AddAsync(NewsEventVm vm, string rootPath);
        Task UpdateAsync(NewsEventVm vm, string rootPath);
        Task DeleteAsync(int id);
    }
}
