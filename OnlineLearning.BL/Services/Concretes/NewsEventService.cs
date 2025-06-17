using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.Helpers.Exictance;
using OnlineLearning.Core.ViewModels;
using OnlineLearning.DAL.Repositories.Abstracts;

namespace OnlineLearning.BL.Services.Concretes
{
    public class NewsEventService : INewsEventService
    {
        private readonly INewsEventRepository _neweventrepository;

        public NewsEventService(INewsEventRepository neweventrepository)
        {
            _neweventrepository = neweventrepository;
        }

        public async Task AddAsync(NewsEventVm vm, string rootPath)
        {
           

            var entity = new NewsEvent
            {
                Title = vm.Title,
                Description = vm.Description,
                ImagePath = FileCreateExtension.CreateFile(vm.ImageFile, rootPath, "//Imagess//"),
                Date = vm.Date
            };

            await _neweventrepository.AddAsync(entity);
            await _neweventrepository.SaveAllChangesAsync();
          
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _neweventrepository.GetByIdAsync(id);
            if (existing == null) return;

            // Faylı da sil
            existing.ImagePath?.RemoveFile("wwwroot", "//Imagess//");
            await _neweventrepository.DeleteAsync(existing);
        }

        public async Task<List<NewsEventVm>> GetAllAsync()
        {
            var list = await _neweventrepository.GetAllAsync();
            return list.Select(e => new NewsEventVm
            {
                Id = e.Id,
                Title = e.Title,
                Description = e.Description,
                ImagePath = e.ImagePath,
                Date = e.Date
            }).ToList();
        }

        public async Task<NewsEventVm?> GetByIdAsync(int id)
        {
            var item = await _neweventrepository.GetByIdAsync(id);
            if (item == null) return null;

            return new NewsEventVm
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                ImagePath = item.ImagePath,
                Date = item.Date
            };
        }

        
        public async Task UpdateAsync(NewsEventVm vm, string rootPath)
        {
            var existing = await _neweventrepository.GetByIdAsync(vm.Id);
            if (existing == null) return;

            if (vm.ImageFile != null)
            {
                existing.ImagePath?.RemoveFile(rootPath, "//Imagess//");
                existing.ImagePath = vm.ImageFile.CreateFile(rootPath, "//Imagess//");
            }

            existing.Title = vm.Title;
            existing.Description = vm.Description;
            existing.Date = vm.Date;

            await _neweventrepository.UpdateAsync(existing);
            await _neweventrepository.SaveAllChangesAsync();
        }
    }
}
