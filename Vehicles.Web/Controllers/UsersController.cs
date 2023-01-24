using Business.Interfaces.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Vehicles.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _roleService;

        public UsersController(IUserService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task AddNewUserAsync()
        {

        }

        [HttpPost("{roleName}")]
        public async Task AddRoleAsync(string roleName)
        {
           
        }

        [HttpPost("{userId}/{roleName}")]
        public async Task SetUserRole(string userId, string roleName)
        {
            
        }

    }
}
