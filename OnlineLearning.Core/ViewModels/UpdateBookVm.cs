using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineLearning.Core.Enums;

namespace OnlineLearning.Core.ViewModels
{
    public class UpdateBookVm
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int PageCount { get; set; }
        public IFormFile? ImgUrl { get; set; } = null;
        public IFormFile? PdfFile { get; set; }

        public string Img { get; set; }  
         public string Pdf{  get; set; }
        public int? CategoryId { get; set; }
        public int? AuthorId { get; set; }

        public BookGenre Genre { get; set; }

    }
}
