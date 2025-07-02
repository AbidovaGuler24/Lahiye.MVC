using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineLearning.Core.ViewModels
{
    public class MomentEditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? ImageFile { get; set; }  // Yeni şəkil yükləmək üçün
        public string? ExistingImagePath { get; set; } // Mövcud şəkilin yolu
    }
}
