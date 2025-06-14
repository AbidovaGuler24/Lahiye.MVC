using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Enums;

namespace OnlineLearning.Core.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int PageCount { get; set; }

        public string PdfUrl { get; set; }
        public string ImgUrl { get; set; }

        public BookGenre Genre { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; } = null!;

        
        public int? AuthorId { get; set; }
        public Author? Author { get; set; } = null!;
    }
}
