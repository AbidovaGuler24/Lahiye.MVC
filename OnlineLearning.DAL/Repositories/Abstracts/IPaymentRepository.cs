using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.DAL.Repositories.Abstracts
{
    public interface IPaymentRepository
    {
        void Add(Payment payment);
        Payment GetById(int id);
        Task<List<BasketItem>> GetBasketItemsByUserIdAsync(string userId);
        Task AddPurchasedBookAsync(PurchasedBook purchasedBook);
        Task ClearBasketAsync(string userId);
    }
}
