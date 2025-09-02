using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.Helpers.Exictance;
using OnlineLearning.Core.ViewModels;
using OnlineLearning.DAL.Repositories.Abstracts;
using OnlineLearning.DAL.Repositories.Concretes;

namespace OnlineLearning.BL.Services.Concretes
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<string> AddBlogAsync(AddBlogVm vm, string wwwroot)
        {


            var blog = new Blog
            {
                Description = vm.Description,
                Img = FileCreateExtension.CreateFile(vm.ImageFile, wwwroot, "\\imagess\\")
            };

            await _blogRepository.AddAsync(blog);
            await _blogRepository.SaveAllChangesAsync();
            return "Added";



        }

        public async Task DeleteBlogAsync(int id)
        {
            

            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog != null)
            {
                if (!string.IsNullOrEmpty(blog.Img))
                {
                    blog.Img.RemoveFile("wwwroot", "imagess");
                }
                
            }
            await _blogRepository.DeleteAsync(blog);
            await _blogRepository.SaveAllChangesAsync();
        }

        public async Task<List<BlogVm>> GetAllBlogsAsync()
        {
            var blogs = await _blogRepository.GetAllAsync();

            var result = new List<BlogVm>();
            foreach (var blog in blogs)
            {
                result.Add(new BlogVm
                {
                    Id = blog.Id,
                    Description = blog.Description,
                    Img = blog.Img
                });
            }
            return result;
        }

        public async Task<BlogVm?> GetBlogByIdAsync(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null) return null;

            return new BlogVm
            {
                Id = blog.Id,
                Description = blog.Description,
                Img = blog.Img
            };
        }

        public async Task UpdateBlogAsync(UpdateBlogVm vm, string wwwroot)
        {
            var blog = await _blogRepository.GetByIdAsync(vm.Id);
            if (blog == null) return;

            if (vm.ImageUrl != null)
                blog.Img?.RemoveFile(wwwroot, "imagess");
            blog.Img = FileCreateExtension.CreateFile(vm.ImageFile, wwwroot, "\\Imagess\\");

            //// Yeni şəkil yüklənibsə
            //if (vm.ImageFile != null)
            //{
            //    // Köhnə şəkli sil
            //    blog.Img?.RemoveFile(wwwroot, "imagess");

            //    // Yeni şəkli yüklə və url-i təyin et
            //    blog.Img = vm.ImageFile.CreateFile(wwwroot, "imagess");
            //}

            blog.Description = vm.Description;

            await _blogRepository.UpdateAsync(blog);
            await _blogRepository.SaveAllChangesAsync();
        }



    }

}
