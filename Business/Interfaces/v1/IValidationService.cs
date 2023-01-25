using Persistence.Entities.v1;

namespace Business.Interfaces.v1
{
    public interface IValidationService
    {
        bool ExistingEmail(string email);

        bool ExistingRole(string roleName);

        Task<ApplicationUser> GetUserAsync(Guid userId);
    }
}
