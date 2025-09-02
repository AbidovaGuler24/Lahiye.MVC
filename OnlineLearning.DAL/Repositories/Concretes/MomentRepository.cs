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
    public class MomentRepository : IMomentRepository
    {
        private readonly AppDbContext _context;

        public MomentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Moment>> GetAllAsync()
        {
            return await _context.Moments
        .Include(m => m.Comments) 
        .ToListAsync();
        }

        public async Task<Moment?> GetByIdAsync(int id)
        {
            return await _context.Moments.Include(x=>x.Comments).Where(x=>x.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddAsync(Moment moment)
        {
            await _context.Moments.AddAsync(moment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Moment moment)
        {
            _context.Moments.Update(moment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Moment moment)
        {
            _context.Moments.Remove(moment);
            await _context.SaveChangesAsync();
        }
    }

}
