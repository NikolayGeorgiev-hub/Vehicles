namespace Business.Models.v1.Vehicles
{
    public class UpdateResponse : BaseResponse
    {
        public int EngineCapacity { get; init; }

        public int VehicleAge { get; init; }

        public string Town { get; set; }

        public string Purpose { get; init; }

    }
}
