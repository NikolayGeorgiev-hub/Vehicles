namespace Business.Models.v1
{
    public class UpdateResponse
    {
        public bool IsUpdated { get; set; }

        public string Reason { get; set; }

        public int EngineCapacity { get; init; }

        public int VehicleAge { get; init; }

        public string Town { get; set; }

        public string Purpose { get; init; }

    }
}
