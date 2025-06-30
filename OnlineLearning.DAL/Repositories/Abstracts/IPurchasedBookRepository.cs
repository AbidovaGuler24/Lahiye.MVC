using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.DAL.Repositories.Abstracts
{
    public interface IPurchasedBookRepository
    {
        Task AddAsync(PurchasedBook purchasedBook);
        Task<bool> ExistsAsync(string userId, int bookId);
        Task<List<PurchasedBook>> GetPurchasedBooksByUserIdAsync(string userId);
    }
}
