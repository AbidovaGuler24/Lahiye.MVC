using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineLearning.Core.ViewModels
{
    public class CafeMenuItemVM
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
       
        public IFormFile? ImageFile { get; set; }
        public string? ImagePath { get; set; }
    }
}
