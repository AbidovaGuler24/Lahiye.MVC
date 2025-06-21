using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.ViewModels;
using OnlineLearning.DAL.Repositories.Abstracts;
using OnlineLearning.Core.Helpers.Exictance;
using OnlineLearning.DAL.Repositories.Concretes;

namespace OnlineLearning.BL.Services.Concretes
{
    public class LibraryItemService : ILibraryItemService
    {
        private readonly ILibraryItemRepository _repo;
        private readonly string _audioFolder = "audio";
        private readonly string _imageFolder = "images";
        public LibraryItemService(ILibraryItemRepository repo)
        {
            _repo = repo;
        }

        public async Task AddAsync(LibraryItemAddVM vm, string rootPath)
        {
            var entity = new LibraryItem
            {
                Title = vm.Title,
                Author = vm.Author,
                Description = vm.Description,
                AgeCategory = vm.AgeCategory,
                //AudioFilePath = vm.AudioFile?.CreateFile(rootPath, _audioFolder),
                AudioFilePath=FileCreateExtension.CreateFile(vm.AudioFile,rootPath,"\\audio\\"),
                //ImageFilePath = vm.ImageFile?.CreateFile(rootPath, _imageFolder)
                ImageFilePath=FileCreateExtension.CreateFile(vm.ImageFile,rootPath,"\\imagess\\"),
            };

            await _repo.AddAsync(entity);
            await _repo.SaveAllChangesAsync();
        }
        

        public async Task DeleteAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            //if (item != null)
            //{
            //    item.AudioFilePath?.RemoveFile("wwwroot", _audioFolder);
            //    item.ImageFilePath?.RemoveFile("wwwroot", _imageFolder);
            //}

            await _repo.DeleteAsync(id);
        }

        public async Task<List<LibraryItemVM>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return items.Select(i => new LibraryItemVM
            {
                Id = i.Id,
                Title = i.Title,
                Author = i.Author,
                Description = i.Description,
                AgeCategory = i.AgeCategory,
                ExistingAudioPath = i.AudioFilePath,
                ExistingImagePath = i.ImageFilePath
            }).ToList();
        }

        public async Task<LibraryItemUpdateVM?> GetByIdAsync(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return null;

            return new LibraryItemUpdateVM
            {
                Id = item.Id,
                Title = item.Title,
                Author = item.Author,
                Description = item.Description,
                AgeCategory = item.AgeCategory,
                ExistingAudioPath = item.AudioFilePath,
                ExistingImagePath = item.ImageFilePath
            };
        }
        public async Task UpdateAsync(LibraryItemUpdateVM vm, string rootPath)
        {
            var existing = await _repo.GetByIdAsync(vm.Id);
            if (existing == null) throw new Exception("Kitab tapılmadı");

            existing.Title = vm.Title;
            existing.Author = vm.Author;
            existing.Description = vm.Description;
            existing.AgeCategory = vm.AgeCategory;

            if (vm.AudioFile != null)
            {
                existing.AudioFilePath?.RemoveFile(rootPath, "audio");
                existing.AudioFilePath = FileCreateExtension.CreateFile(vm.AudioFile, rootPath, "\\audio\\");
               
            }

            if (vm.ImageFile != null)
            {
                existing.ImageFilePath?.RemoveFile(rootPath, "imagess");
                existing.ImageFilePath = FileCreateExtension.CreateFile(vm.ImageFile, rootPath, "\\imagess\\");
            }

            await _repo.UpdateAsync(existing);
        }

       
    }
}
