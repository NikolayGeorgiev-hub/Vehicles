using Business.Interfaces.v1;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Persistence.Entities.v1;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Business.Constants;

namespace Business.Implementations.v1
{
    public class UserService : IUserService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public IdentityResult IdentityResult { get; set; }

        public UserService(
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IConfiguration configuration)

        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
       
        public async Task CreateUserAsync()
        {
            await _userManager
                .CreateAsync(new ApplicationUser
                {
                    UserName = "TestUser",
                    Email = "test@.com",
                    EmailConfirmed = true
                },
                    password: "123456");
        }

        public async Task CreateNewRoleAsync(string roleName)
        {
            if (await this.RoleExistsAsync(roleName) || string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException(string.Format(ErrorMessage.RoleNameExists, roleName));
            }

            IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole(roleName.ToLower()));


        }

        public async Task SetUserRoleAsync(string userId, string roleName)
        {
            ApplicationUser user = await this.GetCurrentUserAsync(userId);
            bool existingRoleName = await this.RoleExistsAsync(roleName);
            this.ThrowNotFoundUser(user, userId);

            if (!existingRoleName)
            {
                throw new ArgumentNullException(string.Format(ErrorMessage.NotFoundRole, roleName));
            }

            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<bool> SignInAsync(string userId)
        {
            ApplicationUser user = await this.GetCurrentUserAsync(userId);
            this.ThrowNotFoundUser(user, userId);

            SignInResult result = await _signInManager
                .PasswordSignInAsync(user, "123456", false, false);

            //var claimsPrincipal = await this.AddClaimsAsync(user);
            //var result = _signInManager.IsSignedIn(claimsPrincipal);

            return result.Succeeded;
        }

        public async Task<string> SignOutAsync(string userId)
        {
            ApplicationUser user = await this.GetCurrentUserAsync(userId);
            this.ThrowNotFoundUser(user, userId);

            await _signInManager.SignOutAsync();
            return user.UserName;
        }

        private async Task<bool> RoleExistsAsync(string roleName)
        {
            bool result = await _roleManager.RoleExistsAsync(roleName.ToLower());
            return result;
        }

        private async Task<ApplicationUser> GetCurrentUserAsync(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        private void ThrowNotFoundUser(ApplicationUser user, string userId)
        {
            if (user is null || string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(string.Format(ErrorMessage.NotFoundUser, userId));
            }
        }

        private async Task<ClaimsPrincipal> AddClaimsAsync(ApplicationUser user)
        {
            var claimsFactory = _signInManager.ClaimsFactory;
            ClaimsPrincipal claimsPrincipal = await claimsFactory.CreateAsync(user);

            return claimsPrincipal;
        }

    }
}
