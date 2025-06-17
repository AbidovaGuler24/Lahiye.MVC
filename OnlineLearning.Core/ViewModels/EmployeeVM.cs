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
    public class EmployeeVM
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Position { get; set; }

        public string? PhotoPath { get; set; }

        public IFormFile? PhotoFile { get; set; }

        public List<EmployeeCommentVM>? Comments { get; set; }
    }
}
