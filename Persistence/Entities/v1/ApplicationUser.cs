using Microsoft.AspNetCore.Identity;

namespace Persistence.Entities.v1
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ICollection<Vehicle> Vehicles { get; set; }

        public ICollection<ApplicationRole> Roles { get; set; }

    }
}
