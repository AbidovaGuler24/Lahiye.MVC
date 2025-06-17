using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Enums;

namespace OnlineLearning.Core.Entities
{
    public class CafeMenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        

        public string PhotoPath { get; set; }

    }
}
