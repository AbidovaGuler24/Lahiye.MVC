using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.DAL.Repositories.Abstracts
{
    public interface ICommentRepository
    {
        Task AddAsync(Comment comment);
        Task<List<Comment>> GetByMomentIdAsync(int momentId);
        Task<Comment> GetByIdAsync(int id);
        Task DeleteAsync(Comment comment);
    }
}
