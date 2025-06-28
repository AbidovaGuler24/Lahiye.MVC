using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.Core.Entities;
using OnlineLearning.DAL.Context;
using OnlineLearning.DAL.Repositories.Abstracts;

namespace OnlineLearning.DAL.Repositories.Concretes
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddToCartAsync(CartItem item)
        {
            var exists = await _context.CartItems
                                       .FirstOrDefaultAsync(c => c.UserId == item.UserId && c.PaidBookId == item.PaidBookId);

            if (exists == null)
            {
                await _context.CartItems.AddAsync(item);
                await _context.SaveChangesAsync();
            }
          
        }

        public async Task ClearCartAsync(string userId)
        {
            var items = _context.CartItems.Where(c => c.UserId == userId);
            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public async Task<CartItem> GetCartItemAsync(string userId, int bookId)
        {
         
            return await _context.CartItems
                                 .FirstOrDefaultAsync(c => c.UserId == userId && c.PaidBookId == bookId);
        }
        

        public async Task<List<CartItem>> GetCartItemsAsync(string userId)
        {
            return await _context.CartItems
                                 .Include(c => c.PaidBook)
                                 .Where(c => c.UserId == userId)
                                 .ToListAsync();
        }

        public async Task RemoveFromCartAsync(string userId, int bookId)
        {
            var item = await _context.CartItems
                                     .FirstOrDefaultAsync(c => c.UserId == userId && c.PaidBookId == bookId);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            _context.CartItems.Update(cartItem);
            await _context.SaveChangesAsync();
        }
    }
}
