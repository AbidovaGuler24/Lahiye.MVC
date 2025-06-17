using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Core.ViewModels
{
    public class PaidBookBuyVm
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
