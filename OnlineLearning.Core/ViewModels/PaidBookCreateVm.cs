using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineLearning.Core.ViewModels
{
    public class PaidBookCreateVm
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int PageCount { get; set; }
        public IFormFile ImgFile { get; set; }
        public IFormFile  PdfFile { get; set; }
        public decimal Price { get; set; }

        public int CategoryId { get; set; } 

        public IEnumerable<SelectListItem>? Categories { get; set; } = new List<SelectListItem>();
    }
}
