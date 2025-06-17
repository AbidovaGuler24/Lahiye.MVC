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
            await _cafeMenuRepository.DeleteAsync(id);
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
            if (existingEntity == null)
                return;
                    var imagePath = menuItemVM.ImagePath;

            if (menuItemVM.ImageFile != null)
            {
                existingEntity.PhotoPath = FileCreateExtension.CreateFile(menuItemVM.ImageFile, wwwroot, "\\Imagess\\");
            }

            // Digər sahələri yenilə
            existingEntity.Name = menuItemVM.Name;
            existingEntity.Price = menuItemVM.Price;

            // Məlumatı yenilə və yadda saxla
            await _cafeMenuRepository.UpdateAsync(existingEntity);
            await _cafeMenuRepository.SaveAllChangesAsync();
          
            //if (menuItemVM.ImagePath != null) 
            //{
            //    imagePath = FileCreateExtension.CreateFile(menuItemVM.ImageFile, wwwroot, "\\Imagess\\");
            //}
            //var entity = new CafeMenuItem
            //{
            //    Id = menuItemVM.Id,
            //    Name = menuItemVM.Name,
            //    Price = menuItemVM.Price,

            //    PhotoPath = imagePath,

            //};

            //await _cafeMenuRepository.UpdateAsync(entity);
        }
    }
}
