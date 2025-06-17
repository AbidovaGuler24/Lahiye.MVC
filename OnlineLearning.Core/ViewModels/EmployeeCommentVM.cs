using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Core.ViewModels
{
    public class EmployeeCommentVM
    {
        public class EmployeeCommentVm
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Şərh məzmunu boş ola bilməz")]
            public string Content { get; set; }

            public DateTime CreatedAt { get; set; }

            public string UserId { get; set; }

            public string UserName { get; set; }
        }
    }
}
