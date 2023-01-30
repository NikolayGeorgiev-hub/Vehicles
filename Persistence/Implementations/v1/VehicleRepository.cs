using Microsoft.EntityFrameworkCore;
using Persistence.Context.v1;
using Persistence.Entities.v1.Vehicles;
using Vehicles.Data.Interfaces.v1;

namespace Persistence.Implementations.v1
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VehicleRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Vehicle entity)
        {
            _dbContext.Add(entity);
        }

        public void Delete(Vehicle entity)
        {
            _dbContext.Remove(entity);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            Vehicle? vehicle = await GetByIdAsync(id);
            if (vehicle is not null)
            {
                Delete(vehicle);
            }
        }

        public async Task<IList<Vehicle>> GetAllAsync()
        {
            return await _dbContext.Vehicles
                .Include(x => x.Town)
                .Include(x => x.VehicleType)
                .Include(x => x.Owner)
                .ToListAsync();
        }

        public Task<Vehicle?> GetByIdAsync(Guid id)
        {
            return _dbContext.Vehicles
                .Include(x => x.VehicleType)
                .Include(x => x.Town)
                .Include(x => x.Owner)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateByIdAsync(Guid id)
        {
            Vehicle? vehicle = await this.GetByIdAsync(id);
            if (vehicle is not null)
            {
                Update(vehicle);
            }
        }

        public Vehicle Update(Vehicle entity)
        {
            _dbContext.Update(entity);
            return entity;
        }

        public async Task<bool> SaveChangesAsync()
        {
            var result = await _dbContext.SaveChangesAsync();
            return result == 1 ? true : false;

        }

        public async Task<bool> IsValidVehicleTypeAsync(Guid typeId)
        {
            var result = await _dbContext.VehicleTypes.AnyAsync(x => x.Id == typeId);
            return result;
        }

        public IQueryable<Vehicle> VehicleQuery()
        {
            return _dbContext.Vehicles.AsQueryable();
        }

    }
}
