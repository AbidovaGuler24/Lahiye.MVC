using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.ViewModels;

namespace OnlineLearning.BL.Services.Abstracts
{
    public interface IAuthorService
    {
        Task<List<AuthorVm>> GetAllAuthorsAsync();
        Task<AuthorVm?> GetAuthorByIdAsync(int id);
        Task<string> AddAuthorAsync(AddAuthorVm mod, string wwwroot);
        Task UpdateAuthorAsync(UpdateAuthorVm vm, string wwwroot);
        Task DeleteAuthorAsync(int id);
        
    }
}
