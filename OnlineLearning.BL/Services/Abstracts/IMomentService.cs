using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.ViewModels;

namespace OnlineLearning.BL.Services.Abstracts
{
    public interface IMomentService
    {
        Task<List<Moment>> GetAllAsync();
        Task<Moment?> GetByIdAsync(int id);
        Task AddMomentAsync(MomentCreateViewModel vm, string webRootPath);
        Task UpdateMomentAsync(int id, MomentEditViewModel vm, string webRootPath);
        Task DeleteMomentAsync(int id, string webRootPath);
    }

}
