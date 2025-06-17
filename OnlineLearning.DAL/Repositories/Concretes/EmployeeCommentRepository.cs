//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using OnlineLearning.Core.Entities;
//using OnlineLearning.DAL.Context;
//using OnlineLearning.DAL.Repositories.Abstracts;

//namespace OnlineLearning.DAL.Repositories.Concretes
//{
//    public class EmployeeCommentRepository : IEmployeeCommentRepository
//    {
//        private readonly AppDbContext _context;

//        public EmployeeCommentRepository(AppDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<EmployeeComment>> GetCommentsByEmployeeIdAsync(int employeeId)
//        {
//            return await _context.EmployeeComments
//                .AsNoTracking()
//                .Include(c => c.Employee)
//                .Where(c => c.EmployeeId == employeeId)
//            .ToListAsync();
//        }

//        public async Task AddCommentAsync(EmployeeComment comment)
//        {
//            await _context.EmployeeComments.AddAsync(comment);
//            await _context.SaveChangesAsync();
//        }
//    }
//}
