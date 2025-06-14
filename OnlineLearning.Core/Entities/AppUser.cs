using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineLearning.Core.Enums;

namespace OnlineLearning.Core.Entities
{
    public class AppUser : IdentityUser
    {
        
        public string Name { get; set; }

        public string Surname { get; set; }

    }
}
