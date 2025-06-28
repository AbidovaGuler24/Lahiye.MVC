using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.DAL.Repositories.Abstracts
{
    public interface ICartRepository
    {
        Task<List<CartItem>> GetCartItemsAsync(string userId);
        Task AddToCartAsync(CartItem item);
        Task RemoveFromCartAsync(string userId, int bookId);
        Task ClearCartAsync(string userId);
        Task UpdateCartItemAsync(CartItem cartItem);

        Task<CartItem> GetCartItemAsync(string userId, int bookId);
    }
}
