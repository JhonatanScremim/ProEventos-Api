using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Interfaces;
using ProEventos.Application.ViewModels;

namespace ProEventos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetUserAsync(string userName)
        {
            try
            {
                return Ok(await _userService.GetUserByUserNameAsync(userName));
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(UserViewModel userViewModel)
        {
            try
            {
                if(await _userService.UserExists(userViewModel.UserName))   
                    return BadRequest("User already exists!");
                
                var user = await _userService.CreateUserAsync(userViewModel);

                if(user == null)
                    return BadRequest("Failed to created");
                
                return Ok(user);
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserViewModel userViewModel)
        {
            try
            {
                var user = await _userService.GetUserByUserNameAsync(userViewModel.UserName);
                if(user == null)
                    return Unauthorized("Invalid User");

                var result = await _userService.CheckUserPasswordAsync(user, userViewModel.Password);
                if(!result.Succeeded)
                    return Unauthorized("Ivalid Password");

                return Ok(new 
                {
                    userName = user.UserName,
                    firstName = user.FirstName,
                    token = _tokenService.CreateToken(user).Result
                });
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }
    }
}