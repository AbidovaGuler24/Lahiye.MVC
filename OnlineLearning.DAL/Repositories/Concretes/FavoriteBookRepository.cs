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
    public class FavoriteBookRepository : IFavoriteBookRepository
    {
        private readonly AppDbContext _context;

        public FavoriteBookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FavoriteBook>> GetAllAsync(string userId)
        {
            return await _context.FavoriteBooks
                .Include(f => f.Book)
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        public async Task<FavoriteBook> GetByIdAsync(int id)
        {
            return await _context.FavoriteBooks.FindAsync(id);
        }

        public async Task AddAsync(FavoriteBook favoriteBook)
        {
            await _context.FavoriteBooks.AddAsync(favoriteBook);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var favorite = await GetByIdAsync(id);
            if (favorite != null)
            {
                _context.FavoriteBooks.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<FavoriteBook> GetByUserAndBookAsync(string userId, int bookId)
        {
            return await _context.FavoriteBooks
                .FirstOrDefaultAsync(f => f.UserId == userId && f.BookId == bookId);
        }

        public async Task SaveAllChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
