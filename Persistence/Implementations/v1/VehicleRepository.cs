using Microsoft.EntityFrameworkCore;
using Persistence.Context.v1;
using Persistence.Entities.v1;
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
                .ToListAsync();
        }

        public Task<Vehicle?> GetByIdAsync(Guid id)
        {
            return _dbContext.Vehicles
                .Include(x => x.VehicleType)
                .Include(x => x.Town)
                .FirstOrDefaultAsync(x => x.Id == id);
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

        public IQueryable<Vehicle> VehicleQuery()
        {
            return _dbContext.Vehicles.AsQueryable();
        }

    }
}
