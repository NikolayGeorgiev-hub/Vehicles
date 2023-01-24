using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Business.Interfaces.v1
{
    public interface IUserService
    {
        Task CreateUserAsync();

        Task CreateNewRoleAsync(string roleName);

        Task SetUserRoleAsync(string userId, string roleName);

        Task<bool> SignInAsync(string userId);

        Task<string> SignOutAsync(string userId);
    }
}
