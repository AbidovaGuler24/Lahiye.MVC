using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Core.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public string UserId { get; set; } // Login olan user üçün
        public int BookId { get; set; }
        public int Quantity { get; set; } = 1;

        public PaidBook Book { get; set; }
    }
}
