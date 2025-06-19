using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stripe.Checkout;

namespace OnlineLearning.DAL.Repositories.Abstracts
{
    public interface IPaymentService
    {
        Session CreateCheckoutSession(decimal amount, string successUrl, string cancelUrl);
    }
}
