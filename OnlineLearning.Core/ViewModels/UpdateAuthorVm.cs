using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineLearning.Core.ViewModels
{
    public class UpdateAuthorVm
    {
        public int Id { get; set; }  
        public string FullName { get; set; } = null!;
        public string? Biography { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
