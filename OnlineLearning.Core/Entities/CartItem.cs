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
        public string UserId { get; set; } 
        public int? PaidBookId { get; set; }
        public PaidBook? PaidBook { get; set; }
        public string? Img { get; set; }

        public int Quantity { get; set; } = 1;

       

    }
}
