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
    public class CafeMenuRepository : ICafeMenuRepository
    {
        private readonly AppDbContext _context;

        public CafeMenuRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(CafeMenuItem menuItem)
        {
            await _context.CafeMenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var menuItem = await _context.CafeMenuItems.FindAsync(id);
            if (menuItem != null)
            {
                _context.CafeMenuItems.Remove(menuItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CafeMenuItem>> GetAllAsync()
        {
            return await _context.CafeMenuItems.ToListAsync();
        }

        public async Task<List<CafeMenuItem>> GetByCategoryAsync(int categoryId)
        {
            return await _context.CafeMenuItems.ToListAsync();
        }

        public async Task<CafeMenuItem> GetByIdAsync(int id)
        {
            return await _context.CafeMenuItems.FindAsync(id);
        }

        public async Task SaveAllChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CafeMenuItem menuItem)
        {
            _context.CafeMenuItems.Update(menuItem);
            await _context.SaveChangesAsync();
        }
    }
}
