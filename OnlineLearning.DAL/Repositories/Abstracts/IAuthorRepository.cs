using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.DAL.Repositories.Abstracts
{
    public interface IAuthorRepository
    {
        Task AddAsync(Author author);
        Task<List<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task UpdateAsync(Author author);
        Task DeleteAsync(Author author);
        Task SaveAllChangesAsync();
    }
}
