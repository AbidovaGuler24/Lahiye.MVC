using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.Helpers.Exictance;
using OnlineLearning.Core.ViewModels;
using OnlineLearning.DAL.Repositories.Abstracts;

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
                await _blogRepository.DeleteAsync(blog);
                await _blogRepository.SaveAllChangesAsync();
            }
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

            if (vm.ImageFile != null)
            {
                var imageName = $"{Guid.NewGuid()}{Path.GetExtension(vm.ImageFile.FileName)}";
                var path = Path.Combine(wwwroot, "uploads", imageName);

                var uploadDir = Path.Combine(wwwroot, "uploads");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await vm.ImageFile.CopyToAsync(stream);
                }

                blog.Img = imageName;
            }

            blog.Description = vm.Description;

            await _blogRepository.UpdateAsync(blog);
            await _blogRepository.SaveAllChangesAsync();
        }
    }
}
