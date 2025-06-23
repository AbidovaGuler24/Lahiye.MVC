using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem>? Categories { get; set; } = new List<SelectListItem>();
    }
}
