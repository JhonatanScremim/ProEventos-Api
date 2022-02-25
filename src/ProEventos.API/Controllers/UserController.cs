using System.Security.Claims;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Interfaces;
using ProEventos.Application.ViewModels;
using ProEventos.API.Extensions;

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

        [HttpGet]
        public async Task<IActionResult> GetUserAsync()
        {
            try
            {
                return Ok(await _userService.GetUserByUserNameAsync(ClaimsPrincipalExtensions.GetUserName(User)));
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
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            try
            {
                var user = await _userService.GetUserByUserNameAsync(loginViewModel.UserName);
                if(user == null)
                    return Unauthorized("Invalid User");

                var result = await _userService.CheckUserPasswordAsync(user, loginViewModel.Password);
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

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateUser(UserUpdateViewModel userUpdateViewModel)
        {
            try
            {
                var user = await _userService.GetUserByUserNameAsync(User.GetUserName());
                if(user == null)
                    return Unauthorized("Invalid User");

                userUpdateViewModel.Id = User.GetUserId();
                
                var response = await _userService.UpdateUserAsync(userUpdateViewModel);
                if(response == null)
                    return NoContent();
                
                return Ok(response);
            }
            catch(Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                "Error: " + e.Message);
            }
        }
    }
}