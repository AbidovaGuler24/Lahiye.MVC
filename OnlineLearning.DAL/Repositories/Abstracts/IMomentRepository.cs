using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.DAL.Repositories.Abstracts
{
    public interface IMomentRepository
    {
        
            Task<List<Moment>> GetAllAsync();
            Task<Moment?> GetByIdAsync(int id);
            Task AddAsync(Moment moment);
            Task UpdateAsync(Moment moment);
            Task DeleteAsync(Moment moment);
        
    }
}
