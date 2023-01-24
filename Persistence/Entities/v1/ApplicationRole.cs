using Microsoft.AspNetCore.Identity;

namespace Persistence.Entities.v1
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

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
