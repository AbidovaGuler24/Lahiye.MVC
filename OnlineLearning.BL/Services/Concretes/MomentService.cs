using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.ViewModels;
using OnlineLearning.DAL.Repositories.Abstracts;
using OnlineLearning.Core.Helpers.Exictance;


namespace OnlineLearning.BL.Services.Concretes
{
    public class MomentService : IMomentService
    {
        private readonly IMomentRepository _repository;
     

        public MomentService(IMomentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Moment>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Moment?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddMomentAsync(MomentCreateViewModel vm, string webRootPath)
        {

            var moment = new Moment
            {
                Title = vm.Title,
                Description = vm.Description,
                ImagePath = FileCreateExtension.CreateFile(vm.ImageFile, webRootPath,"\\imagess\\")
            };

            await _repository.AddAsync(moment);
        }

        public async Task UpdateMomentAsync(int id, MomentEditViewModel vm, string webRootPath)
        {
            var existingMoment = await _repository.GetByIdAsync(id);
            if (existingMoment == null)
                throw new Exception("Moment not found");

            if (vm.ImageFile != null)
            {
                // Əvvəlki şəkli sil
                existingMoment.ImagePath?.RemoveFile(webRootPath, "imagess");

                // Yeni şəkli yüklə və yolunu təyin et
                string newImageName = FileCreateExtension.CreateFile(vm.ImageFile, webRootPath, "\\Imagess\\");
                existingMoment.ImagePath = newImageName;
            }
           

            existingMoment.Title = vm.Title;
            existingMoment.Description = vm.Description;

            await _repository.UpdateAsync(existingMoment);
        }


        public async Task DeleteMomentAsync(int id, string webRootPath)
        {
            var existingMoment = await _repository.GetByIdAsync(id);
            if (existingMoment == null)
                throw new Exception("Moment not found");

            existingMoment.ImagePath?.RemoveFile(webRootPath, "imagess");

            await _repository.DeleteAsync(existingMoment);
        }
    }



}
