using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.BL.Services.Abstracts
{
    public interface ICartService
    {
        Task<List<CartItem>> GetCartItemsAsync(string userId);
        Task<bool> AddToCartAsync(string userId, int bookId, int quantity = 1);
        Task RemoveFromCartAsync(string userId, int bookId);
        Task ClearCartAsync(string userId);

        Task<bool> AddSingleItemAsync(string userId, int bookId);
        Task<bool> RemoveSingleItemAsync(string userId, int bookId);

    }
}
