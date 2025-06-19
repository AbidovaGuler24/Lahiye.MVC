using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.DAL.Repositories.Abstracts
{
    public interface IFavoriteBookRepository
    {
        Task<List<FavoriteBook>> GetAllAsync(string userId);
        Task<FavoriteBook> GetByIdAsync(int id);
        Task AddAsync(FavoriteBook favoriteBook);
        Task DeleteAsync(int id);
        Task<FavoriteBook> GetByUserAndBookAsync(string userId, int bookId);
        Task SaveAllChangesAsync();
    }
}
