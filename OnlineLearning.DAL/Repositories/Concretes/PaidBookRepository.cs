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
    public class PaidBookRepository : IPaidBookRepository
    {
        private readonly AppDbContext _context;

        public PaidBookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(PaidBook book)
        {
            await _context.PaidBooks.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PaidBook book)
        {
            _context.PaidBooks.Remove(book);
            await _context.SaveChangesAsync();
        }

        public  async Task<List<PaidBook>> GetAllAsync()
        {
            return await _context.PaidBooks.ToListAsync();
        }

        public async Task<PaidBook?> GetByIdAsync(int id)
        {
            return await _context.PaidBooks
         .Include(pb => pb.Category) 
         .FirstOrDefaultAsync(pb => pb.Id == id);
        }
        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }

      
        public async Task<List<PaidBook>> GetPaidBooksByCategoryIdAsync(int? categoryId, int excludeBookId)
        {
            return await _context.PaidBooks
          .Where(b => b.CategoryId == categoryId && b.Id != excludeBookId)
          .ToListAsync();
        }
        public async Task<int> SaveAllChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PaidBook book)
        {
            _context.PaidBooks.Update(book);
            await _context.SaveChangesAsync(); ;
        }

        
    }
}
