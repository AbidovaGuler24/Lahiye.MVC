using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineLearning.Core.ViewModels
{
    public class AddNewsEventVm
    {
        [Required] 
        public string Title { get; set; }
        [Required] 
        public string Description { get; set; }
        [Required] 
        public DateTime Date { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
