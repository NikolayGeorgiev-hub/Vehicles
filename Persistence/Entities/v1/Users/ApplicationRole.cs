using Microsoft.AspNetCore.Identity;

namespace Persistence.Entities.v1.Users
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName)
            : base(roleName)
        {
        }
    }
}
