using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Core.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? Biography { get; set; }

        public string? ImagePath { get; set; }


        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
