using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Core.Entities
{
    public class BasketItem
    {
        public int Id { get; set; }

        public string UserId { get; set; }  
        public int BookId { get; set; }     

        public int Count { get; set; }     
        public decimal Price { get; set; }  
    }
}
