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
    public class LibraryItemRepository : ILibraryItemRepository
    {
        private readonly AppDbContext _context;

        public LibraryItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LibraryItem item)
        {
            _context.LibraryItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.LibraryItems.FindAsync(id);
            if (item != null)
            {
                _context.LibraryItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<LibraryItem>> GetAllAsync()
        {
            return await _context.LibraryItems.ToListAsync();
        }

        public async  Task<LibraryItem?> GetByIdAsync(int id)
        {
            return await _context.LibraryItems.FindAsync(id);
        }

        public async Task SaveAllChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LibraryItem item)
        {
            _context.LibraryItems.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
