using Microsoft.AspNetCore.Identity;
using Persistence.Entities.v1.Vehicles;

namespace Persistence.Entities.v1.Users
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
