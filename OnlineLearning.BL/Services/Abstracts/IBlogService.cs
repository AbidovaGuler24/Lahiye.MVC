using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.ViewModels;

namespace OnlineLearning.BL.Services.Abstracts
{
    public interface IBlogService
    {
        Task<List<BlogVm>> GetAllBlogsAsync();
        Task<BlogVm?> GetBlogByIdAsync(int id);
        Task<string> AddBlogAsync(AddBlogVm mod, string wwwroot);
        Task UpdateBlogAsync(UpdateBlogVm vm, string wwwroot);
        Task DeleteBlogAsync(int id);
    }
}
