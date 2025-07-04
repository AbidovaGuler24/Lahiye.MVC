using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.ViewModels;
using OnlineLearning.DAL.Repositories.Abstracts;

namespace OnlineLearning.BL.Services.Concretes
{
    public class FavoriteBookService : IFavoriteBookService
    {
        private readonly IFavoriteBookRepository _favoriteBookRepository;

        public FavoriteBookService(IFavoriteBookRepository favoriteBookRepository)
        {
            _favoriteBookRepository = favoriteBookRepository;
        }
        public async Task<string> AddFavoriteAsync(string userId, int bookId)
        {
            var exists = await _favoriteBookRepository.GetByUserAndBookAsync(userId, bookId);
            if (exists == null)
            {
                var favorite = new FavoriteBook
                {
                    UserId = userId,
                    BookId = bookId
                };
                await _favoriteBookRepository.AddAsync(favorite);
                
                return "Added";
            }
            return "null";
        }
        public async Task<List<FavoriteBookVM>> GetFavoritesAsync(string userId)
        {
            var favorites = await _favoriteBookRepository.GetAllAsync(userId);

            return favorites.Select(f => new FavoriteBookVM
            {
                Id = f.Id,
                BookId = f.BookId,
                BookTitle = f.Book?.Title ?? "No Title",
                CoverImage = f.Book?.Img ?? "no-image.png",
                Price = f.Book?.Price ?? 0
            }).ToList();
        }

        public async Task<bool> IsFavoriteAsync(string userId, int bookId)
        {
            var fav = await _favoriteBookRepository.GetByUserAndBookAsync(userId, bookId);
            return fav != null;
        }

        public async Task RemoveFavoriteAsync(int id)
        {
            await _favoriteBookRepository.DeleteAsync(id);

            
        }

    }
}
