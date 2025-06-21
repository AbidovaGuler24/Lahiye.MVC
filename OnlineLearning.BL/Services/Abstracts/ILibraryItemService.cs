using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.ViewModels;

namespace OnlineLearning.BL.Services.Abstracts
{
    public interface ILibraryItemService
    {
        Task<List<LibraryItemVM>> GetAllAsync();
        Task<LibraryItemUpdateVM?> GetByIdAsync(int id);
        Task AddAsync(LibraryItemAddVM vm, string rootPath);
        Task UpdateAsync(LibraryItemUpdateVM vm, string rootPath);
        Task DeleteAsync(int id);
    }
}
