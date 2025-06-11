using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;

namespace OnlineLearning.DAL.Repositories.Abstracts
{
    public interface IUserRepository
    {
        
            void Add(User user);
            User? GetByUsername(string username);
            bool ValidateCredentials(string username, string password);
            List<User> GetAll();
        
    }
}
