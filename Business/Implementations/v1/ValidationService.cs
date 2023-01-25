using Business.Interfaces.v1;
using Microsoft.EntityFrameworkCore;
using Persistence.Context.v1;
using Persistence.Entities.v1;

namespace Business.Implementations.v1
{
    public class ValidationService : IValidationService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ValidationService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        public bool ExistingEmail(string email)
        {
            var result = _applicationDbContext.Users
                .Select(x => x.Email)
                .ToArray()
                .Any(x => x == email);

            return result;
        }

        public bool ExistingRole(string roleName)
        {
            var result = _applicationDbContext.Roles
                .Select(x => x.Name)
                .ToArray()
                .Any(x => x == roleName);

            return result;
        }

        public async Task<ApplicationUser> GetUserAsync(Guid userId)
        {
            var user = await _applicationDbContext.Users
                .FirstOrDefaultAsync(x => x.Id == userId);

            return user;
        }
    }
}
