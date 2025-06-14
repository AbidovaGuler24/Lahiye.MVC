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
using OnlineLearning.DAL.Repositories.Concretes;

namespace OnlineLearning.BL.Services.Concretes
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
       

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
          
        }
        public async Task<string> AddAuthorAsync(AddAuthorVm mod, string wwwroot)
        {
            var author = new Author
            {
                FullName = mod.FullName,
                Biography = mod.Biography,
                ImagePath= FileCreateExtension.CreateFile(mod.ImageFile, wwwroot, "\\imagess\\")

            };


            await _authorRepository.AddAsync(author);
            await _authorRepository.SaveAllChangesAsync();
            return "Author uğurla əlavə olundu.";
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) throw new Exception("Author not found");



            await _authorRepository.DeleteAsync(author);
            await _authorRepository.SaveAllChangesAsync();
        }



        public async Task<List<AuthorVm?>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
           
            var authorVms = new List<AuthorVm>();
            foreach (var author in authors)
            {
                authorVms.Add(new AuthorVm
                {
                    Id = author.Id,
                    FullName = author.FullName,
                    Biography = author.Biography,
                    ImagePath=author.ImagePath,

                });
            }
            return authorVms;
        }
        public async Task<AuthorVm?> GetAuthorByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null) return null;

            return new AuthorVm
            {
                Id = author.Id,
                FullName = author.FullName,
                Biography = author.Biography,
            };
        }



        public async Task UpdateAuthorAsync(UpdateAuthorVm vm, string wwwroot)
        {
            var author = await _authorRepository.GetByIdAsync(vm.Id);
            if (author == null) throw new Exception("Author not found");


            author.Biography = vm.Biography;
            author.FullName = vm.FullName;
            author.Id = vm.Id;

            if (vm.ImageFile != null)
            {
                author.ImagePath?.RemoveFile(wwwroot, "Images");
                author.ImagePath = FileCreateExtension.CreateFile(vm.ImageFile, wwwroot, "\\Imagess\\");

            }

            await _authorRepository.UpdateAsync(author);
            await _authorRepository.SaveAllChangesAsync();
        }
    }
}
