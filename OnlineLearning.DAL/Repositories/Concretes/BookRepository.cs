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
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;  

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public  async Task DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books
       
        .Include(b => b.Category)
        .ToListAsync();
        }
        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
        
        .Include(b => b.Category)
        .FirstOrDefaultAsync(b => b.Id == id);
        }

     

        public  async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            
        }

        public async Task SaveAllChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<List<Book>> GetByCategoryIdAsync(int categoryId, int excludeBookId)
        {
            return await _context.Books
                .Where(b => b.CategoryId == categoryId && b.Id != excludeBookId)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }

        public async Task<List<Book>> GetBooksByCategoryIdAsync(int? categoryId, int excludeBookId)
        {
            return await _context.Books
          .Where(b => b.CategoryId == categoryId && b.Id != excludeBookId)
          .ToListAsync();
        }
    }
}
