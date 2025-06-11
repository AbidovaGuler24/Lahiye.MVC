using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.BL.Services.Abstracts
{
    public interface IUserService
    {
        string Register(string username, string password);
        string Login(string username, string password);
        string Logout();
        bool IsLoggedIn { get; }
        string CurrentUserInfo { get; }
    }
}
