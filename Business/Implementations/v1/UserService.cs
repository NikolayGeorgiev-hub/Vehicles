using Business.Interfaces.v1;
using Business.Models.v1.Errors;
using Business.Models.v1.Roles;
using Business.Models.v1.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence.Entities.v1.Users;
using Persistence.Interfaces.v1;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using static Business.Constants;

namespace Business.Implementations.v1;

public class UserService : IUserService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public IdentityResult IdentityResult { get; set; }

    public UserService(
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IUserRepository userRepository,
        IConfiguration configuration)

    {
        _roleManager = roleManager;
        _userManager = userManager;
        _signInManager = signInManager;
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<UserResponese> CreateUserAsync(UserRegistrationRequest registrationRequest)
    {
        IdentityResult result = new IdentityResult();
        var response = new UserResponese();

        var existingEmail = await _userRepository.ExistingEmailAsync(registrationRequest.Email);

        if (existingEmail)
        {
            result = IdentityResult.Failed(new
            IdentityError
            {
                Description = string.Format(LoginMessage.AlreadyTakenEmail, registrationRequest.Email)
            });

            response = new UserResponese
            {
                Email = registrationRequest.Email,
                IsSucceeded = false,
                Error = new Error
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Description = this.GetErrorDescription(result)
                }
            };

            return response;
        }

        result = await _userManager
            .CreateAsync(new ApplicationUser
            {
                UserName = registrationRequest.UserName,
                Email = registrationRequest.Email,
                EmailConfirmed = true
            },
                password: registrationRequest.Password);

        if (!result.Succeeded)
        {
            response = new UserResponese
            {
                Email = registrationRequest.Email,
                Error = new Error
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Description = result.Errors.FirstOrDefault().Description
                }
            };
        }
        else
        {
            response = new UserResponese { Email = registrationRequest.Email, IsSucceeded = true };
        }

        return response;
    }

    public async Task<RoleResponse> CreateNewRoleAsync(RoleRequest requestRole)
    {
        IdentityResult result = await _roleManager
            .CreateAsync(new ApplicationRole(requestRole.RoleName));

        var response = new RoleResponse();

        if (result.Succeeded)
        {
            response = new RoleResponse
            {
                RoleName = requestRole.RoleName,
                IsSucceeded = true
            };
        }
        else
        {
            response = new RoleResponse
            {
                RoleName = requestRole.RoleName,
                IsSucceeded = false,
                Error = new Error
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Description = this.GetErrorDescription(result)
                }
            };
        }

        return response;
    }

    public async Task<AddToRoleResponse> SetUserRoleAsync(AddToRoleRequest roleRequest)
    {
        var user = await _userRepository.FindeByIdAsync(roleRequest.UserId);
        var response = new AddToRoleResponse();

        if (user is null)
        {
            response = new AddToRoleResponse
            {
                UserId = roleRequest.UserId,
                IsSucceeded = false,
                Error = new Error
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Description = string.Format(ErrorMessage.NotFoundUser, roleRequest.UserId)
                }
            };

            return response;
        }
        else
        {
            bool roleNameExists = await _roleManager.RoleExistsAsync(roleRequest.Name.ToLower());
            if (!roleNameExists)
            {
                response = new AddToRoleResponse
                {
                    IsSucceeded = false,
                    Error = new Error
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Description = string.Format(ErrorMessage.NotFoundRole, roleRequest.Name)
                    }
                };

                return response;
            }
            else
            {
                var result = await _userManager.AddToRoleAsync(user, roleRequest.Name);
                var userRoles = await _userManager.GetRolesAsync(user);

                if (result.Succeeded)
                {
                    response = new AddToRoleResponse
                    {
                        RoleName = roleRequest.Name,
                        IsSucceeded = true,
                        UserName = user.UserName,
                        UserId = user.Id,
                        UserRoles = userRoles,
                    };

                    return response;
                }

                response = new AddToRoleResponse
                {
                    IsSucceeded = false,
                    RoleName = roleRequest.Name,
                    UserId = user.Id,
                    UserName = user.UserName,
                    UserRoles = userRoles,
                    Error = new Error
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        Description = this.GetErrorDescription(result)
                    },
                };
            }
        }

        return response;

    }

    public async Task<UserResponese> SignInAsync(UserLoginRequest loginRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);
        var response = new UserResponese();

        if (user is null)
        {
            response = new UserResponese
            {
                IsSucceeded = false,
                Error = new Error
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Description = string.Format(ErrorMessage.NotFoundUser, loginRequest.Email)
                }
            };

            return response;
        }

        SignInResult result = await _signInManager
            .PasswordSignInAsync(user, loginRequest.Password, false, false);

        if (!result.Succeeded)
        {
            response = new UserResponese
            {
                Email = loginRequest.Email,
                IsSignedIn = false,
                IsSucceeded = false,
                Error = new Error
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Description = ErrorMessage.FailedLogin
                }
            };

            return response;
        }

        var token = await this.GenerateJSONWebToken(user);
        response = new UserResponese
        {
            IsSignedIn = token is not null ? true : false,
            Email = user.Email,
            IsSucceeded = true,
            Token = token,
        };

        return response;
    }

    private async Task<string> GenerateJSONWebToken(ApplicationUser userInfo)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
           new Claim(JwtRegisteredClaimNames.Sub, userInfo.Id.ToString()),
           new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
          
        }.ToList();

        var roles = await _userManager.GetRolesAsync(userInfo);
        claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));


        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
          _configuration["Jwt:Issuer"],
          claims,
          null,
          expires: DateTime.Now.AddMinutes(120),
          signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GetErrorDescription(IdentityResult result)
    {
        var description = result.Errors.FirstOrDefault().Description;
        return description;
    }

}
