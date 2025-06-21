using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Core.Entities
{
    public class LibraryItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public AgeCategory AgeCategory { get; set; }
        public string? AudioFilePath { get; set; }
        public string? ImageFilePath { get; set; }
       
    }


    public enum AgeCategory
    {
        Children = 0,
        Adult = 1
    }
}
