using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineLearning.Core.Entities;
using OnlineLearning.DAL.Context;
using OnlineLearning.DAL.Repositories.Abstracts;

namespace OnlineLearning.DAL.Repositories.Concretes
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public async Task AddPurchasedBookAsync(PurchasedBook purchasedBook)
        {
            await _context.PurchasedBooks.AddAsync(purchasedBook);
            await _context.SaveChangesAsync();
        }

        public async Task ClearBasketAsync(string userId)
        {
            var items = _context.BasketItems.Where(b => b.UserId == userId);
            _context.BasketItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

        public async Task<List<BasketItem>> GetBasketItemsByUserIdAsync(string userId)
        {
            return await _context.BasketItems
        .Where(b => b.UserId == userId)
        .ToListAsync();
        }

        public Payment GetById(int id)
        {
            return _context.Payments.FirstOrDefault(p => p.Id == id);
        }
    }
}
