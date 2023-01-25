using Business.Models.v1.Vehicles;

namespace Business.Interfaces.v1
{
    public interface IVehicleService
    {
        Task<VehicleResponse> GetByIdAsync(Guid id);
        
        Task<VehicleResponse> ThrowException();

        Task<IList<VehicleResponse>> GetAllAsync();

        Task<VehicleResponse> CreateAsync(VehicleRequest model);

        Task<IList<VehicleResponse>> GetVehiclesInRangeAsync(int min, int max);


        Task<UpdateResponse> UpdateAsync(Guid id, VehicleRequest model);

        Task<IList<VehicleResponse>> GetByTownAsync(string townName);

        Task<DeleteRespons> DeleteAsync(Guid id);
    }
}
