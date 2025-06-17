//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using OnlineLearning.BL.Services.Abstracts;
//using OnlineLearning.Core.Entities;
//using OnlineLearning.DAL.Context;
//using OnlineLearning.DAL.Repositories.Abstracts;

//namespace OnlineLearning.BL.Services.Concretes
//{
//    public class EmployeeCommentService : IEmployeeCommentService
//    {
//        private readonly IEmployeeCommentRepository _commentRepository;
//        private readonly IEmployeeRepository _employeeRepository;

//        public EmployeeCommentService(IEmployeeCommentRepository commentRepository, IEmployeeRepository employeeRepository)
//        {
//            _commentRepository = commentRepository;
//            _employeeRepository = employeeRepository;
//        }

//        public async Task AddAsync(EmployeeCommentCreateVm vm, string userId)
//        {
//            var employee = await _employeeRepository.GetByIdAsync(vm.EmployeeId);
//            if (employee == null)
//                throw new Exception("Employee not found");

//            var comment = new EmployeeComment
//            {
//                Content = vm.Content,
//                EmployeeId = vm.EmployeeId,
//                UserId = userId,
//                CreatedAt = DateTime.Now
//            };

//            await _commentRepository.AddAsync(comment);
//        }

//        public async Task DeleteAsync(int id)
//        {
//            var comment = await _commentRepository.GetByIdAsync(id);
//            if (comment == null)
//                throw new Exception("Comment not found");

//            await _commentRepository.DeleteAsync(comment);
//        }

//        public async Task<IEnumerable<EmployeeCommentVm>> GetAllByEmployeeIdAsync(int employeeId)
//        {
//            var comments = await _commentRepository.GetAllByEmployeeIdAsync(employeeId);
//            var commentVms = new List<EmployeeCommentVm>();

//            foreach (var c in comments)
//            {
//                commentVms.Add(new EmployeeCommentVm
//                {
//                    Id = c.Id,
//                    Content = c.Content,
//                    CreatedAt = c.CreatedAt,
//                    EmployeeId = c.EmployeeId,
//                    UserId = c.UserId,
//                    UserName = c.User.UserName // user adını da göstərmək üçün
//                });
//            }

//            return commentVms;
//        }

//        public async Task<EmployeeCommentVm> GetByIdAsync(int id)
//        {
//            var comment = await _commentRepository.GetByIdAsync(id);
//            if (comment == null) return null;

//            return new EmployeeCommentVm
//            {
//                Id = comment.Id,
//                Content = comment.Content,
//                CreatedAt = comment.CreatedAt,
//                EmployeeId = comment.EmployeeId,
//                UserId = comment.UserId,
//                UserName = comment.User.UserName
//            };
//        }

//        public async Task UpdateAsync(EmployeeCommentUpdateVm vm)
//        {
//            var comment = await _commentRepository.GetByIdAsync(vm.Id);
//            if (comment == null)
//                throw new Exception("Comment not found");

//            comment.Content = vm.Content;
//            await _commentRepository.UpdateAsync(comment);
//        }
//    }
