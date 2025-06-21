using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.Core.ViewModels
{
    public class LibraryItemAddVM
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public AgeCategory AgeCategory { get; set; }
        public IFormFile? AudioFile { get; set; }
        public IFormFile? ImageFile { get; set; }
        
    }
}
