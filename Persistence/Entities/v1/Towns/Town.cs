using Persistence.Entities.v1.Vehicles;

namespace Persistence.Entities.v1.Towns
{
    public class Town : BaseEntity
    {
        public string Name { get; set; }

        public string Postcode { get; set; }

        public IEnumerable<Vehicle> Vehicles { get; set; }

    }
}
