using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.Core.ViewModels
{
    public class MomentViewModel
    {
        public int? Id { get; set; }
        public string? Title { get; set; } = "";
        public string? Description { get; set; } = "";
        public string? ImagePath { get; set; } = "";
        public DateTime? CreatedAt { get; set; }

        public CommentViewModel? Comment { get; set; }
        public List<CommentViewModel>? Comments { get; set; } = new();
    }
}
