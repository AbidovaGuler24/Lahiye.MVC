using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.ViewModels;
using OnlineLearning.DAL.Repositories.Abstracts;
using Stripe;
using Stripe.Climate;

namespace OnlineLearning.BL.Services.Concretes
{
    public class PaymentService:IPaymentService
    {


        private readonly IPaymentRepository _paymentRepository;
        private readonly IConfiguration _configuration;

        public PaymentService(IPaymentRepository paymentRepository, IConfiguration configuration)
        {
            _paymentRepository = paymentRepository;
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        public Payment ProcessStripePayment(PaymentCreateVm vm)
        {
            var options = new ChargeCreateOptions
            {
                Amount = (long)(vm.Amount * 100), 
                Currency = "usd",
                Description = "Book purchase",
                Source = vm.StripeToken,
            };

            var service = new ChargeService();
            var charge = service.Create(options);

            var payment = new Payment
            {
                UserId = vm.UserId,
                Amount = vm.Amount,
                PaymentDate = DateTime.UtcNow,
                PaymentStatus = charge.Status == "succeeded" ? "Success" : "Failed"
            };

            _paymentRepository.Add(payment);

            return payment;
        }
        public string CreatePayment(List<BasketItem> items, string stripeEmail, string userId, string userName, string userSurname)
        {
            decimal total = 0;
            foreach (var item in items)
            {
                total += item.Count * item.Price;
            }

            var customerService = new CustomerService();
            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Name = $"{userName} {userSurname}",
                Phone = "+994553901121" 
            });

            long amountInCents = (long)(total * 100);

            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
            {
                Amount = amountInCents,
                Currency = "azn",
                Customer = customer.Id,
                ReceiptEmail = stripeEmail,
                PaymentMethodTypes = new List<string> { "card" },
            });

            return paymentIntent.Id; 
        }

        public async Task<bool> ConfirmPaymentAndAddPaidBooks(string userId, string paymentIntentId)
        {

            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = await paymentIntentService.GetAsync(paymentIntentId);

            if (paymentIntent.Status == "succeeded")
            {
                
                var basketItems = await _paymentRepository.GetBasketItemsByUserIdAsync(userId);

                foreach (var item in basketItems)
                {
                    var purchasedBook = new PurchasedBook
                    {
                        BookId = item.BookId,
                        UserId = userId,
                        PurchaseDate = DateTime.UtcNow
                    };

                    await _paymentRepository.AddPurchasedBookAsync(purchasedBook);
                }

                
                await _paymentRepository.ClearBasketAsync(userId);

                return true;
            }

            return false;
        }
    }
}
