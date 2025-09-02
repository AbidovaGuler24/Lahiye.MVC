using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.Helpers.Exictance;
using OnlineLearning.Core.ViewModels;
using OnlineLearning.DAL.Repositories.Abstracts;
using OnlineLearning.DAL.Repositories.Concretes;

namespace OnlineLearning.BL.Services.Concretes
{
    public class CafeMenuService : ICafeMenuService
    {
        private readonly ICafeMenuRepository _cafeMenuRepository;

        public CafeMenuService(ICafeMenuRepository cafeMenuRepository)
        {
            _cafeMenuRepository = cafeMenuRepository;
        }

        public async Task AddAsync(CafeMenuItemVM menuItemVM, string wwwroot)
        {
           

            var entity = new CafeMenuItem
            {
                Name = menuItemVM.Name,
                Price = menuItemVM.Price,

                PhotoPath = FileCreateExtension.CreateFile(menuItemVM.ImageFile, wwwroot, "\\imagess\\")
            };
            await _cafeMenuRepository.AddAsync(entity);
            await _cafeMenuRepository.SaveAllChangesAsync();
          
        }

        public async Task DeleteAsync(int id)
        {
            var entities = await _cafeMenuRepository.GetByIdAsync(id);
            if (entities != null)
            {
              
                entities.PhotoPath?.RemoveFile("wwwroot", "imagess");
            }

           
            await _cafeMenuRepository.DeleteAsync(id);
            await _cafeMenuRepository.SaveAllChangesAsync();
        }

        public async Task<List<CafeMenuItemVM>> GetAllAsync()
        {
            var entities = await _cafeMenuRepository.GetAllAsync();
            return entities.Select(entity => new CafeMenuItemVM
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                
                ImagePath = entity.PhotoPath,
              
                
            }).ToList();
        }

        public async Task<List<CafeMenuItemVM>> GetByCategoryAsync(int categoryId)
        {
            var entities = await _cafeMenuRepository.GetByCategoryAsync(categoryId);
            return entities.Select(entity => new CafeMenuItemVM
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
               
                ImagePath = entity.PhotoPath,
           
               
            }).ToList();
        }

        public async Task<CafeMenuItemVM> GetByIdAsync(int id)
        {
            var entity = await _cafeMenuRepository.GetByIdAsync(id);
            if (entity == null)
                return null;

            return new CafeMenuItemVM
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                
                ImagePath = entity.PhotoPath,
                
               
            };
        }

        public async Task UpdateAsync(CafeMenuItemVM menuItemVM, string wwwroot)
        {
            var existingEntity = await _cafeMenuRepository.GetByIdAsync(menuItemVM.Id);
            if (existingEntity == null) return;

            if (menuItemVM.ImageFile != null)
            {
                // Köhnə şəkli sil
                existingEntity.PhotoPath?.RemoveFile(wwwroot, "Imagess");

                // Yeni şəkli əlavə et
                existingEntity.PhotoPath = FileCreateExtension.CreateFile(menuItemVM.ImageFile, wwwroot, "\\Imagess\\");
            }

            existingEntity.Name = menuItemVM.Name;
            existingEntity.Price = menuItemVM.Price;

            await _cafeMenuRepository.UpdateAsync(existingEntity);
            await _cafeMenuRepository.SaveAllChangesAsync();
        }

    }
}
