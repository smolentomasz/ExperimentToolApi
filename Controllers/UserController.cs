using System.Linq;
using System;
using System.Security.Claims;
using ExperimentToolApi.Interfaces;
using ExperimentToolApi.Models;
using ExperimentToolApi.Secure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ExperimentToolApi.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [AllowAnonymous]
        [HttpPost("/tool/users/login")]
        public IActionResult Login([FromHeader(Name = "Username")] string username, [FromHeader(Name = "Password")] string password)
        {
            if (userRepository.FindByUsername(username))
            {
                User loggedUser = userRepository.GetUserByUsername(username);
                if (BCryptUtilities.passwordMatch(password, loggedUser.Password))
                {
                    LoginResponse loginTokenResponse = userRepository.GetUserToken(loggedUser);

                    return Ok(loginTokenResponse);
                }
                else
                {
                    return Unauthorized("Password is not matching!");
                }

            }
            else
            {
                return BadRequest("User with this username doesn't exist in database!");
            }
        }
        [AllowAnonymous]
        [HttpPost("/tool/users/refresh")]
        public IActionResult Refresh([FromHeader(Name = "Token")] string token, [FromHeader(Name = "RefreshToken")] string refreshToken)
        {
            string savedRefreshToken = userRepository.getRefreshToken(token);
            if (savedRefreshToken != refreshToken)
            {
                return Unauthorized("Invalid refresh token!");
            }
            else
            {
                return Ok(userRepository.GetRefreshedUserToken(token));
            }
        }
    }
}