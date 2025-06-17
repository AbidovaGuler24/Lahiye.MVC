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
            return await _context.PaidBooks.FindAsync(id);
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
