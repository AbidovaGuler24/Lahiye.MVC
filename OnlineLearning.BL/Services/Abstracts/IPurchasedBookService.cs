using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.BL.Services.Abstracts
{
    public interface IPurchasedBookService
    {
        Task AddPurchasedBookAsync(string userId, int bookId);
        Task<bool> IsBookPurchasedAsync(string userId, int bookId);
        Task<List<PurchasedBook>> GetPurchasedBooksByUserIdAsync(string userId);
    }
}
