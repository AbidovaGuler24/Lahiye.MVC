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
    public class NewsEventRepository : INewsEventRepository
    {
        private readonly AppDbContext _context;

        public NewsEventRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(NewsEvent newsEvent)
        {
            await _context.NewsEvents.AddAsync(newsEvent);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(NewsEvent newsEvent)
        {
            _context.NewsEvents.Remove(newsEvent);
            await _context.SaveChangesAsync();
        }

        public async Task<List<NewsEvent>> GetAllAsync()
        {
            return await _context.NewsEvents.ToListAsync();
        }

        public async Task<NewsEvent?> GetByIdAsync(int id)
        {
            return await _context.NewsEvents.FindAsync(id);
        }

        public async Task SaveAllChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(NewsEvent newsEvent)
        {
            _context.NewsEvents.Update(newsEvent);
            await _context.SaveChangesAsync();
        }
    }
}
