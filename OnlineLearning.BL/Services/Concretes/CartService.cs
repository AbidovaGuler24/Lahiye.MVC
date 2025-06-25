using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.Entities;
using OnlineLearning.DAL.Context;
using OnlineLearning.DAL.Repositories.Abstracts;

namespace OnlineLearning.Core.Services
{
    public class CartService : ICartService
    {
      
            private readonly ICartRepository _cartRepository;

            public CartService(ICartRepository cartRepository)
            {
                _cartRepository = cartRepository;
            }
        public async Task<bool> AddToCartAsync(string userId, int bookId, int quantity = 1)
        {
            var existingItem = await _cartRepository.GetCartItemAsync(userId, bookId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                await _cartRepository.UpdateCartItemAsync(existingItem);
                return false; 
            }

            var cartItem = new CartItem
            {
                UserId = userId,
                PaidBookId = bookId,
                Quantity = quantity,
                
            };

            await _cartRepository.AddToCartAsync(cartItem);
            return true;
        }

        public async Task ClearCartAsync(string userId)
        {
            await _cartRepository.ClearCartAsync(userId);
        }

        public async Task<List<CartItem>> GetCartItemsAsync(string userId)
        {
            return await _cartRepository.GetCartItemsAsync(userId);
        }

        public async Task RemoveFromCartAsync(string userId, int bookId)
        {
            await _cartRepository.RemoveFromCartAsync(userId, bookId);
        }

       
    }
}
