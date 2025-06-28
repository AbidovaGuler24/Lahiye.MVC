using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.ViewModels;

namespace OnlineLearning.BL.Services.Abstracts
{
    public interface IPaymentService
    {
        Payment ProcessStripePayment(PaymentCreateVm vm);
        Task<bool> ConfirmPaymentAndAddPaidBooks(string userId, string paymentIntentId);

        string CreatePayment(List<BasketItem> items, string stripeEmail, string userId, string userName, string userSurname);
    }
}
