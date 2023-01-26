using Persistence.Entities.v1;

namespace Persistence.Interfaces.v1
{
    public interface IUserRepository
    {
        Task<ApplicationUser> FindeByEmailAsync(string email);

        Task<ApplicationUser> FindeByIdAsync(Guid id);

        Task<IList<ApplicationRole>> GetUserRolesAsync(Guid id);

        Task<bool> ExistingUserAsync(Guid id);

        Task<bool> ExistingEmailAsync(string email);
    }
}
