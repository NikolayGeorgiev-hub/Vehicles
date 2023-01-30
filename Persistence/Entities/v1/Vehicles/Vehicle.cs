using Persistence.Entities.v1.Towns;
using Persistence.Entities.v1.Users;

namespace Persistence.Entities.v1.Vehicles
{
    public class Vehicle : BaseEntity
    {
        public int EngineCapacity { get; set; }

        public int VehicleAge { get; set; }

        public Guid VehicleTypeId { get; set; }

        public VehicleType VehicleType { get; set; }

        public Guid TownId { get; set; }

        public Town Town { get; set; }

        public Guid OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }

        public Purpose Purpose { get; set; }
    }
}
