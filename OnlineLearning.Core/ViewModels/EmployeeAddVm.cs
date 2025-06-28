using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineLearning.Core.ViewModels
{
    public class EmployeeAddVm
    {
        [Required(ErrorMessage = "Ad daxil edilməlidir.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vəzifə daxil edilməlidir.")]
        public string Position { get; set; }

        [Display(Name = "Şəkil")]
        public IFormFile PhotoFile { get; set; }

        public IFormFile CvFile { get; set; }

    }
}
