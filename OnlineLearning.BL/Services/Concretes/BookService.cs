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
    public class BookService : IBookService
    {
        
        private readonly IBookRepository _bookRepository;
       
        public BookService( IBookRepository bookRepository)
        {
            
            _bookRepository = bookRepository;
        }
        public async Task<string> AddBookAsync(AddBookVm model,string wwwroot)
        {
            
            
            var book = new Book
            {
                Title = model.Title,
                Description = model.Description,
                PageCount = model.PageCount,
                CategoryId = model.CategoryId.HasValue ? (int)model.CategoryId : null,
                AuthorId = model.AuthorId.HasValue ? (int)model.AuthorId : null,

            };

            if (model.ImgUrl != null)
                book.ImgUrl = FileCreateExtension.CreateFile(model.ImgUrl, wwwroot, "\\Imagess\\");

            if (model.PdfFile != null)
                book.PdfUrl = model.PdfFile.CreateFile(wwwroot, "\\Files\\");
            

            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveAllChangesAsync();
            return "BookAdded";

        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            await _bookRepository.DeleteAsync(id);
             await _bookRepository.SaveAllChangesAsync();
            
        }

      

        
        

        public async Task<BookVm?> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return null;
            BookVm bookVm = new BookVm()

            {
                Title = book.Title,
                Description = book.Description,
                PageCount = book.PageCount,
                CategoryId = book.CategoryId.HasValue ? (int)book.CategoryId : null,
                AuthorId = book.AuthorId.HasValue ? (int)book.AuthorId : null,
                Img = book.ImgUrl,
                Pdf = book.PdfUrl



            };
            return bookVm;
        }

        public async Task UpdateBookAsync(UpdateBookVm vm, string wwwroot)
        {
            var book = await _bookRepository.GetByIdAsync(vm.Id);
            if (book == null) return;

            book.Title = vm.Title;
            book.Description = vm.Description;
            book.PageCount = vm.PageCount;
            book.AuthorId = vm.AuthorId;
            book.CategoryId = vm.CategoryId;

            if (vm.ImgUrl != null)
                book.ImgUrl = FileCreateExtension.CreateFile(vm.ImgUrl, wwwroot, "\\Imagess\\");

            if (vm.PdfFile != null)
                book.PdfUrl = vm.PdfFile.CreateFile(wwwroot, "Files");
            else
                book.PdfUrl = vm.Pdf;

            if (vm.Img == null && vm.ImgUrl == null)
                book.ImgUrl = vm.Img;

            await _bookRepository.UpdateAsync(book);
            await _bookRepository.SaveAllChangesAsync();
        }

        public async Task<List<BookVm>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();

            List<BookVm> booksList = new List<BookVm>();
            foreach (var item in books)
            {
                var bookVm = new BookVm()
                {  Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    PageCount = item.PageCount,
                    AuthorId = item.AuthorId,
                    CategoryId = item.CategoryId,
                    Img = item.ImgUrl,
                    Pdf = item.PdfUrl
                };
                booksList.Add(bookVm);
            }
            return booksList;
        }
    }
}
