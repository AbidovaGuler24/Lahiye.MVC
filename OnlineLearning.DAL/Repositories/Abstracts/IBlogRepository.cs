using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.DAL.Repositories.Abstracts
{
    public  interface IBlogRepository
    {
        Task AddAsync(Blog blog);
        Task<List<Blog>> GetAllAsync();
        Task<Blog?> GetByIdAsync(int id);
        Task UpdateAsync(Blog blog);
        Task DeleteAsync(Blog blog);
        Task SaveAllChangesAsync();
    }
}
