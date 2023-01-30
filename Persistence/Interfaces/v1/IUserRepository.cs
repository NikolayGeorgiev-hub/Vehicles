using Persistence.Entities.v1.Users;

namespace Persistence.Interfaces.v1
{
    public interface IUserRepository
    {
        Task<ApplicationUser> FindeByEmailAsync(string email);

        Task<ApplicationUser> FindeByIdAsync(Guid id);

        Task<bool> ExistingUserAsync(Guid id);

        Task<bool> ExistingEmailAsync(string email);
    }
}
