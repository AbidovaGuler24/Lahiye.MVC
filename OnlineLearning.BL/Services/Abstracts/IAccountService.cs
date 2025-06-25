using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLearning.Core.Entities;
using OnlineLearning.Core.ViewModels;

namespace OnlineLearning.BL.Services.Abstracts
{
    public interface IAccountService
    {
        Task<bool> RegisterAsync(RegisterViewModel registerVm);
        Task<bool> LoginAsync(LoginViewModel loginVm);
        Task LogoutAsync();
        Task<AppUser> GetUserByEmailAsync(string email);
        
        Task<IList<string>> GetRolesAsync(AppUser user);
    }
}
