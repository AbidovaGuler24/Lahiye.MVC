using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Core.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string PhotoPath { get; set; }

        public string CvPath { get; set; }
        public string Email { get; set; }
        public bool IsApproved { get; set; }
        public List<EmployeeComment> Comments { get; set; }
    }
}
