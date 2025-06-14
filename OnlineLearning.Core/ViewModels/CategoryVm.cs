using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Core.ViewModels
{
    public class CategoryVm
    {
        public int Id { get; set; }             
        public string Name { get; set; } = null!;        
        public string? Description { get; set; }
    }
}
