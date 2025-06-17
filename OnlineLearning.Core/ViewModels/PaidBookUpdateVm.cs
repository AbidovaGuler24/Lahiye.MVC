using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineLearning.Core.ViewModels
{
    public class PaidBookUpdateVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public IFormFile? ImgFile { get; set; }
        public string? Img { get; set; }
        public IFormFile? PdfFile { get; set; }
        public string? Pdf { get; set; }
        public decimal Price { get; set; }
    }
}
