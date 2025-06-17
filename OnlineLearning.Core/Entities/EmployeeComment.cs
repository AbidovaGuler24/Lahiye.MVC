using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Core.Entities
{
    public class EmployeeComment
    {
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }  

        [Required]
        public string UserId { get; set; }   

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }  

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        
        public Employee Employee { get; set; }
    }
}
