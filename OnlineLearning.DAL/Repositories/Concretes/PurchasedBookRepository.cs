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
    public class PurchasedBookRepository : IPurchasedBookRepository
    {
        private readonly AppDbContext _context;

        public PurchasedBookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PurchasedBook purchasedBook)
        {
            _context.PurchasedBooks.Add(purchasedBook);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string userId, int bookId)
        {
            return await _context.PurchasedBooks
                .AnyAsync(p => p.UserId == userId && p.BookId == bookId);
        }
        public async Task<List<PurchasedBook>> GetPurchasedBooksByUserIdAsync(string userId)
        {
            return await _context.PurchasedBooks
                          .Include(pb => pb.Book)  
                          .Where(pb => pb.UserId == userId)
                          .ToListAsync();
        }
    }
}
