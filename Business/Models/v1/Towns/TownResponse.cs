using Business.Models.v1.Vehicles;

namespace Business.Models.v1.Towns
{
    public class TownResponse
    {
        public string Name { get; set; }

        public string Postcode { get; set; }

        public int CarsCount => Vehicles.Count();

        public IEnumerable<VehicleResponse> Vehicles { get; set; }

    }
}
