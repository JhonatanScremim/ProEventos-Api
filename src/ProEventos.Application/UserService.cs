using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProEventos.Application.Interfaces;
using ProEventos.Application.ViewModels;
using ProEventos.Domain.Identity;
using ProEventos.Repository.Interfaces;

namespace ProEventos.Application
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(UserManager<User> userManager, 
                            SignInManager<User> signInManager, 
                            IMapper mapper, IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<SignInResult> CheckUserPasswordAsync(UserUpdateViewModel user, string password)
        {
            var oldUser = await _userManager.Users
                                .FirstOrDefaultAsync(x => x.UserName.ToLower() == user.UserName.ToLower());
        
            return await _signInManager.CheckPasswordSignInAsync(oldUser, password, false);
        
        }

        public async Task<UserViewModel> CreateUserAsync(UserViewModel user)
        {
            var newUser = _mapper.Map<User>(user);
            var result = await _userManager.CreateAsync(newUser, user.Password);

            if(result.Succeeded)
                return _mapper.Map<UserViewModel>(user);

            return null;
        }

        public async Task<UserUpdateViewModel> GetUserByUserNameAsync(string username)
        {
            var user = await _userRepository.GetUserByUserNameAsync(username);

            if(user == null)
                return null;

            return _mapper.Map<UserUpdateViewModel>(user);
        }

        public async Task<UserUpdateViewModel> UpdateUserAsync(UserUpdateViewModel user)
        {
            var oldUser = await _userRepository.GetUserByUserNameAsync(user.UserName);

            if(oldUser == null)
                return null;

            _mapper.Map(user, oldUser);


            //Para que o usuario se mantenha logado após atualizar a senha 
            // var token = await _userManager.GeneratePasswordResetTokenAsync(oldUser);
            // await _userManager.ResetPasswordAsync(oldUser, token, user.Password);

            _userRepository.Update<User>(oldUser);

            if(await _userRepository.SaveChangesAsync())
                return _mapper.Map<UserUpdateViewModel>(oldUser);

            return null;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }
    }
}