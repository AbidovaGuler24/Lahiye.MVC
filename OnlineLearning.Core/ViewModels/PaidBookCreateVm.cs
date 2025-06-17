using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineLearning.Core.ViewModels
{
    public class PaidBookCreateVm
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public IFormFile ImgFile { get; set; }
        public IFormFile PdfFile { get; set; }
        public decimal Price { get; set; }
    }
}
