using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.Enums;
using OnlineLearning.DAL.Repositories.Abstracts;

namespace OnlineLearning.DAL.Repositories.Concretes
{

    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public void Add(User user) => _users.Add(user);

        public User? GetByUsername(string username) =>
            _users.FirstOrDefault(u => u.Username == username);

        public bool ValidateCredentials(string username, string password) =>
            _users.Any(u => u.Username == username && u.Password == password);

        public List<User> GetAll() => _users;
    }
}
