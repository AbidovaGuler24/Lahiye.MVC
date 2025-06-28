using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Core.ViewModels
{
    public class PaymentCreateVm
    {
        public decimal Amount { get; set; }
        public string StripeToken { get; set; }

        public int UserId { get; set; }
    }

}
