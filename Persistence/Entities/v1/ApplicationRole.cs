using Microsoft.AspNetCore.Identity;

namespace Persistence.Entities.v1
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ICollection<ApplicationUser> Users { get; set; }
    }
}
