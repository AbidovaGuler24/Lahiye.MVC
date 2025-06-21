using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.DAL.Repositories.Abstracts
{
    public interface ILibraryItemRepository
    {
        Task<List<LibraryItem>> GetAllAsync();
        Task<LibraryItem?> GetByIdAsync(int id);
        Task AddAsync(LibraryItem item);
        Task UpdateAsync(LibraryItem item);
        Task DeleteAsync(int id);
        Task SaveAllChangesAsync();
    }
}
