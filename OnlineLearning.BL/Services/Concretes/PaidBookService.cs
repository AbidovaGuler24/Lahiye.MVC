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

    public class PaidBookService : IPaidBookService
    {
        private readonly IPaidBookRepository _paidBookRepository;
        
        public PaidBookService(IPaidBookRepository paidBookRepository)
        {
            _paidBookRepository = paidBookRepository;
        }
        public async Task CreateAsync(PaidBookCreateVm vm, string wwwroot)
        {
            var paidBook = new PaidBook
            {
                Title = vm.Title,
                Description = vm.Description,
                PageCount = vm.PageCount,
                Price = vm.Price,
                Img = vm.ImgFile != null ? vm.ImgFile.CreateFile(wwwroot, "\\imagess\\") : null,
                Pdf = vm.PdfFile != null ? vm.PdfFile.CreateFile(wwwroot, "\\Files\\") : null,
                 CategoryId = vm.CategoryId,
            };

            await _paidBookRepository.CreateAsync(paidBook);
            await _paidBookRepository.SaveAllChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var paidBook = await _paidBookRepository.GetByIdAsync(id);
            if (paidBook == null) return;

          
            paidBook.Img?.RemoveFile( /* burada wwwroot lazım olacaq, lazım olsa parametra əlavə edərik */ "", "\\imagess\\");
            paidBook.Pdf?.RemoveFile( /* burada da wwwroot lazım olacaq */ "", "\\Files\\");

            await _paidBookRepository.DeleteAsync(paidBook);
            await _paidBookRepository.SaveAllChangesAsync();
        }

        public async Task<List<PaidBookVm>> GetAllAsync()
        {
            var paidBooks = await _paidBookRepository.GetAllAsync();

            var list = new List<PaidBookVm>();
            foreach (var item in paidBooks)
            {
                list.Add(new PaidBookVm
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    PageCount = item.PageCount,
                    Img = item.Img,
                    Pdf = item.Pdf,
                    CategoryId = item.CategoryId,
                    Category = item.Category,
                    Price = item.Price
                });
            }
            return list;
        }

        public async Task<List<PaidBookVm>> GetBooksByCategoryIdAsync(int? categoryId, int excludeBookId)
        {
            var books = await _paidBookRepository.GetPaidBooksByCategoryIdAsync(categoryId, excludeBookId);

            var paidbookVms = books.Select(b => new PaidBookVm
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                PageCount = b.PageCount,
                Img = b.Img ?? string.Empty,
                Pdf = b.Pdf,
                CategoryId = b.CategoryId,
                Category = b.Category
            }).ToList();

            return paidbookVms;
        }

        public async Task<PaidBookVm?> GetByIdAsync(int id)
        {
            var paidBook = await _paidBookRepository.GetByIdAsync(id);
            if (paidBook == null) return null;

            return new PaidBookVm
            {
                Id = paidBook.Id,
                Title = paidBook.Title,
                Description = paidBook.Description,
                PageCount = paidBook.PageCount,
                Img = paidBook.Img,
                Pdf = paidBook.Pdf,
                Price = paidBook.Price,
                CategoryId = paidBook.CategoryId,
                Category = paidBook.Category,
            };
        }

        //public async Task<List<PaidBookVm>> GetFilteredAsync(string? search, decimal? minPrice, int? minPage)
        //{
        //    var paidBooks = await _paidBookRepository.GetAllAsync();

        //    if (!string.IsNullOrWhiteSpace(search))
        //        paidBooks = paidBooks.Where(b => b.Title != null && b.Title.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

        //    if (minPrice.HasValue)
        //        paidBooks = paidBooks.Where(b => b.Price >= minPrice.Value).ToList();

        //    if (minPage.HasValue)
        //        paidBooks = paidBooks.Where(b => b.PageCount >= minPage.Value).ToList();

        //    return paidBooks.Select(b => new PaidBookVm
        //    {
        //        Id = b.Id,
        //        Title = b.Title,
        //        Description = b.Description,
        //        PageCount = b.PageCount,
        //        Img = b.Img,
        //        Pdf = b.Pdf,
        //        Price = b.Price
        //    }).ToList();
        //}

        public async Task UpdateAsync(PaidBookUpdateVm vm, string wwwroot)
        {
            var paidBook = await _paidBookRepository.GetByIdAsync(vm.Id);
            if (paidBook == null) return;

            paidBook.Title = vm.Title;
            paidBook.Description = vm.Description;
            paidBook.PageCount = vm.PageCount;
            paidBook.Price = vm.Price;
            paidBook.CategoryId = vm.CategoryId;
            if (vm.ImgFile != null)
            {
                paidBook.Img?.RemoveFile(wwwroot, "imagess");
                paidBook.Img = vm.ImgFile.CreateFile(wwwroot, "\\imagess\\");
            }
            else if (!string.IsNullOrEmpty(vm.Img))
            {
                paidBook.Img = vm.Img;
            }

            if (vm.PdfFile != null)
            {
                paidBook.Pdf?.RemoveFile(wwwroot, "Files");
                paidBook.Pdf = vm.PdfFile.CreateFile(wwwroot, "\\Files\\");
            }
            else if (!string.IsNullOrEmpty(vm.Pdf))
            {
                paidBook.Pdf = vm.Pdf;
            }

            await _paidBookRepository.UpdateAsync(paidBook);
            await _paidBookRepository.SaveAllChangesAsync();
        }

    }
}
