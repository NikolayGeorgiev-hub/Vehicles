using Business.Interfaces.v1;
using Business.Models.v1.Roles;
using Business.Models.v1.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Persistence.Entities.v1;
using static Business.Constants;

namespace Vehicles.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<IUserService> _logger;

        public UsersController(
            IUserService userService,
            ILogger<IUserService> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("CreateUser")]
        public async Task<UserResponese> AddNewUserAsync(UserRegistrationRequest requestUser)
        {
            try
            {
                var response = await _userService.CreateUserAsync(requestUser);
                if (response.IsSucceeded)
                {
                    _logger.Log(LogLevel.Information, string.Format(LoginMessage.SuccessfulRegistration, response.Email));
                    return response;
                }

                _logger.Log(LogLevel.Information, $"Status code: {response.Error.StatusCode}, Error: {response.Error.Description}");
                return response;


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("CreateRole")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationRoles.AdminRoleName)]
        public async Task<RoleResponse> AddRoleAsync(RoleRequest requestRole)
        {
            try
            {
                var response = await _userService.CreateNewRoleAsync(requestRole);
                if (response.IsSucceeded)
                {
                    _logger.Log(LogLevel.Information, string.Format(LoginMessage.SuccessfulAddRole, response.RoleName));
                    return response;
                }

                _logger.Log(LogLevel.Information, response.Error.Description);
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("SetRole")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationRoles.AdminRoleName)]
        public async Task<AddToRoleResponse> SetUserRole(AddToRoleRequest roleRequest)
        {
            try
            {
                var response = await _userService.SetUserRoleAsync(roleRequest);
                if (response.IsSucceeded)
                {
                    _logger.Log(LogLevel.Information, string.Format(LoginMessage.SuccessfulSetToRole,response.RoleName,response.UserName));
                    return response;
                }

                _logger.Log(LogLevel.Information, response.Error.Description);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("logIn")]
        public async Task<UserResponese> LogInAsync(UserLoginRequest loginRequest)
        {
            try
            {
                var response = await _userService.SignInAsync(loginRequest);
                if (response.IsSucceeded)
                {
                    _logger.Log(LogLevel.Information, string.Format(LoginMessage.Login, response.Email));
                    return response;
                }

                _logger.Log(LogLevel.Information, response.Error.Description);
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw new Exception(ex.Message);
            }
        }

    }
}
