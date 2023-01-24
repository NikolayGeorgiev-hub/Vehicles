using Business.Interfaces.v1;
using Microsoft.AspNetCore.Identity;
using Persistence.Entities.v1;

namespace Business.Implementations.v1
{
    public class UserService : IUserService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResult IdentityResult { get; set; }

        public UserService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

    }
}
