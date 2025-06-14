using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.ViewModels;

namespace OnlineLearning.BL.Services.Abstracts
{
    public interface IBookService
    {
        Task<List<BookVm>> GetAllBooksAsync();
        Task<BookVm?> GetBookByIdAsync(int id);
        Task<string> AddBookAsync(AddBookVm model,string wwwroot );
        Task UpdateBookAsync(UpdateBookVm vm, string wwwroot);
        Task DeleteBookAsync(int id);
        
    }
}
