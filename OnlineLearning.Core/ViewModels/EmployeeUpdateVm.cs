using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OnlineLearning.Core.ViewModels
{
    public class EmployeeUpdateVm
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Position { get; set; }

        public IFormFile Photo { get; set; }  

        public string ExistingPhotoPath { get; set; }
    }
}
