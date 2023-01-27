using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Context.v1;
using Persistence.Entities.v1;
using Persistence.Interfaces.v1;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Persistence.Implementations.v1
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ApplicationUser> FindeByEmailAsync(string email)
        {
            ApplicationUser? user = await _applicationDbContext.Users
                .FirstOrDefaultAsync(x => x.Email == email);

            return user;
        }

        public async Task<ApplicationUser> FindeByIdAsync(Guid id)
        {
            ApplicationUser? user = await _applicationDbContext.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<bool> ExistingUserAsync(Guid id)
        {
            var result = await _applicationDbContext.Users
                .AnyAsync(x => x.Id == id);

            return result;
        }

        public async Task<bool> ExistingEmailAsync(string email)
        {
            var result = await _applicationDbContext.Users
                .Select(x => x.Email.ToLower())
                .AnyAsync(x => x == email.ToLower());

            return result;
        }

    }
}
