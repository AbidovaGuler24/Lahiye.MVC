using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.ViewModels;
using OnlineLearning.DAL.Repositories.Abstracts;

namespace OnlineLearning.BL.Services.Concretes
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repo;

        public CommentService(ICommentRepository repo)
        {
            _repo = repo;
        }

        public async Task AddCommentAsync(CommentViewModel vm)
        {
            var comment = new Comment
            {
                MomentId = vm.MomentId,
                UserName = vm.UserName,
                Content = vm.Content,
                CreatedAt = DateTime.UtcNow
            };
            await _repo.AddAsync(comment);
        }
    }
}
