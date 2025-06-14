using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.ViewModels;

namespace OnlineLearning.BL.Services.Abstracts
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);

        Task AddCategoryAsync(AddCategoryVm model);
        Task UpdateCategoryAsync(UpdateCategoryVm model);
        Task DeleteCategoryAsync(int id);
    }
}

