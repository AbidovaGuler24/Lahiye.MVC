using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.ViewModels;

namespace OnlineLearning.BL.Services.Abstracts
{
    public interface IFavoriteBookService
    {
        Task<List<FavoriteBookVM>> GetFavoritesAsync(string userId);
        Task<string> AddFavoriteAsync(string userId, int bookId);
        Task RemoveFavoriteAsync(int id);
        Task<bool> IsFavoriteAsync(string userId, int bookId);
    }
}
