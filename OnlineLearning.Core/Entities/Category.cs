using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Core.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
       
        public string? Description { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
        public ICollection<PaidBook> PaidBook { get; set; } = new List<PaidBook>();
    }
}
