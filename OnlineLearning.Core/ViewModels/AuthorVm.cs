using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Core.ViewModels
{
    public class AuthorVm
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public string? Biography { get; set; }

        public string? ImagePath { get; set; }
    }
}
