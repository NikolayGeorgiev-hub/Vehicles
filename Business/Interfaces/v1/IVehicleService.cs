using Business.Models.v1.Vehicles;

namespace Business.Interfaces.v1
{
    public interface IVehicleService
    {
        Task<VehicleResponse> CreateAsync(VehicleRequest model, Guid ownerId);

        Task<UpdateResponse> UpdateAsync(Guid vehicleId, Guid ownerId, VehicleRequest model);

        Task<DeleteRespons> DeleteAsync(Guid id,Guid ownerId);

        Task<IList<VehicleResponse>> GetAllAsync();

        Task<VehicleResponse> GetByIdAsync(Guid id);

        Task<IList<VehicleResponse>> GetVehiclesInRangeAsync(int min, int max);

        Task<IList<VehicleResponse>> GetByTownAsync(string townName);

        Task<VehicleResponse> ThrowException();
    }
}
