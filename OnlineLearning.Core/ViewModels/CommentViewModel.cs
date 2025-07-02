using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Core.ViewModels
{
    public class CommentViewModel
    {
        public int MomentId { get; set; }

        [Required(ErrorMessage = "Adınızı daxil edin")]
        public string UserName { get; set; } = "";

        [Required(ErrorMessage = "Şərh boş ola bilməz")]
        [StringLength(1000, MinimumLength = 3)]
        public string Content { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
