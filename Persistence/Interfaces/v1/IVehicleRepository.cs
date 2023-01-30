using Persistence.Entities.v1.Vehicles;

namespace Vehicles.Data.Interfaces.v1
{
    public interface IVehicleRepository
    {
        void Add(Vehicle entity);

        Task<IList<Vehicle>> GetAllAsync();

        IQueryable<Vehicle> VehicleQuery();

        Task<Vehicle?> GetByIdAsync(Guid id);

        Task UpdateByIdAsync(Guid id);

        Vehicle Update(Vehicle entity);

        Task DeleteByIdAsync(Guid id);

        void Delete(Vehicle entity);

        Task<bool> SaveChangesAsync();

        Task<bool> IsValidVehicleTypeAsync(Guid typeId);
    }
}
