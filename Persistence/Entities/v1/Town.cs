namespace Persistence.Entities.v1
{
    public class Town : BaseEntity
    {
        public string Name { get; set; }

        public string Postcode { get; set; }

        public IEnumerable<Vehicle> Vehicles { get; set; }

    }
}
