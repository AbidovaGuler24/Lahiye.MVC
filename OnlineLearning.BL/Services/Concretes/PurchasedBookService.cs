using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.Entities;
using OnlineLearning.DAL.Repositories.Abstracts;

namespace OnlineLearning.BL.Services.Concretes
{
    public class PurchasedBookService : IPurchasedBookService
    {
        private readonly IPurchasedBookRepository _repository;

        public PurchasedBookService(IPurchasedBookRepository repository)
        {
            _repository = repository;
        }

        public async Task AddPurchasedBookAsync(string userId, int bookId)
        {
            var purchasedBook = new PurchasedBook
            {
                UserId = userId,
                BookId = bookId,
                PurchaseDate = DateTime.UtcNow
            };
            await _repository.AddAsync(purchasedBook);
        }

        public async Task<List<PurchasedBook>> GetPurchasedBooksByUserIdAsync(string userId)
        {
            return await _repository.GetPurchasedBooksByUserIdAsync(userId);
        }

        public async Task<bool> IsBookPurchasedAsync(string userId, int bookId)
        {
            return await _repository.ExistsAsync(userId, bookId);
        }
    }
}
