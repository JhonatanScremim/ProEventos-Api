using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProEventos.Application.ViewModels;

namespace ProEventos.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> UserExists(string username);
        Task<UserUpdateViewModel> GetUserByUserNameAsync(string username);
        Task<SignInResult> CheckUserPasswordAsync(UserUpdateViewModel user, string password);
        Task<UserViewModel> CreateUserAsync(UserViewModel user);
        Task<UserUpdateViewModel> UpdateUserAsync(UserUpdateViewModel user); 
    }
}