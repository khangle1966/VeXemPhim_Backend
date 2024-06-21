using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieTicketAPI.Models;
using MovieTicketAPI.Services;
using System;

namespace MovieTicketAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UserService userService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(AuthRequest authRequest)
        {
            var existingUser = _userService.GetByUsername(authRequest.Username);
            if (existingUser != null)
            {
                return BadRequest("Username already exists.");
            }

            var user = new User
            {
                Username = authRequest.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(authRequest.Password),
                Role = "User"
            };

            _userService.Create(user);

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<LoginResponse> Login(AuthRequest authRequest)
        {
            var user = _userService.GetByUsername(authRequest.Username);

            if (user == null)
            {
                _logger.LogWarning("User not found.");
                return Unauthorized("Invalid username or password.");
            }

            if (!BCrypt.Net.BCrypt.Verify(authRequest.Password, user.Password))
            {
                _logger.LogWarning("Password verification failed.");
                return Unauthorized("Invalid username or password.");
            }

            var response = new LoginResponse
            {
                Message = "Login successful",
                Role = user.Role
            };

            return Ok(response);
        }
    }
}
