namespace Business.Models.v1.Vehicles
{
    using Persistence.Entities.v1;

    public class DeleteRespons
    {
        public bool IsDelited { get; set; }

        public string Reason { get; set; }

        public string Purpose { get; set; }

        public string VehicleType { get; set; }

    }
}
