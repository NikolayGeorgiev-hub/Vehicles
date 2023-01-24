using Business.Interfaces.v1;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities.v1;
using System.Linq.Expressions;
using System.Security.Claims;

using static Business.Constants;

namespace Vehicles.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<IUserService> _logger;

        public UsersController(IUserService userService, ILogger<IUserService> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        public async Task AddNewUserAsync()
        {
            await _userService.CreateUserAsync();
        }

        [HttpPost("{roleName}")]
        public async Task AddRoleAsync(string roleName)
        {
            try
            {
                await _userService.CreateNewRoleAsync(roleName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("{userId}/{roleName}")]
        public async Task SetUserRole(string userId, string roleName)
        {
            try
            {
                await _userService.SetUserRoleAsync(userId, roleName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("logIn/{userId}")]
        public async Task LogInAsync(string userId)
        {
            try
            {
                var result = await _userService.SignInAsync(userId);
                var message = string.Format(LoginMessage.Login, result is true ? "Succeeded" : "Not Succeeded");

                _logger.Log(LogLevel.Information, message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("logOut/{userId}")]
        public async Task LogOutAsync(string userId)
        {

            try
            {
                string userName = await _userService.SignOutAsync(userId);
                _logger.Log(LogLevel.Information, string.Format(LoginMessage.Logount, userName));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

    }
}
