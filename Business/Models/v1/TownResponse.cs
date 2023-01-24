namespace Business.Models.v1
{
    public class TownResponse
    {
        public string Name { get; set; }

        public string Postcode { get; set; }

        public int CarsCount => this.Vehicles.Count();

        public IEnumerable<VehicleResponse> Vehicles { get; set; }

    }
}
