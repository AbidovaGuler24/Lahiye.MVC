using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.DAL.Repositories.Abstracts
{
    public interface INewsEventRepository
    {
        Task<List<NewsEvent>> GetAllAsync();
        Task<NewsEvent?> GetByIdAsync(int id);
        Task AddAsync(NewsEvent newsEvent);
        Task UpdateAsync(NewsEvent newsEvent);
        Task DeleteAsync(NewsEvent newsEvent);
        Task SaveAllChangesAsync();
    }
}
