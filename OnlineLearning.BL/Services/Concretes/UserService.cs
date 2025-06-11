using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.BL.Services.Abstracts;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.Enums;
using OnlineLearning.DAL.Repositories.Abstracts;

namespace OnlineLearning.BL.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private User? _currentUser;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return "Username və şifrə boş ola bilməz.";

            if (_userRepository.GetByUsername(username) != null)
                return "Bu istifadəçi adı artıq mövcuddur.";

            var user = new User
            {
                Username = username,
                Password = password,
                role = AssignRole()
            };

            _userRepository.Add(user);
            return $"Qeydiyyat tamamlandı. Rol: {user.role}";
        }

        public string Login(string username, string password)
        {
            if (_userRepository.ValidateCredentials(username, password))
            {
                _currentUser = _userRepository.GetByUsername(username);
                return $"Giriş uğurlu: {_currentUser.Username} ({_currentUser.role})";
            }

            return "İstifadəçi adı və ya şifrə yalnışdır.";
        }

        public string Logout()
        {
            if (_currentUser == null)
                return "Heç bir istifadəçi daxil olmayıb.";

            string name = _currentUser.Username;
            _currentUser = null;
            return $"{name} sistemdən çıxış etdi.";
        }

        public bool IsLoggedIn => _currentUser != null;

        public string CurrentUserInfo => _currentUser != null
            ? $"{_currentUser.Username} ({_currentUser.role})"
            : "Sistemdə istifadəçi yoxdur.";

        private Role AssignRole()
        {
            var count = _userRepository.GetAll().Count;
            if (count == 0)
                return Role.Admin;
            else if (count == 1)
                return Role.Moderator;
            else
                return Role.User;
        }
    }
}
