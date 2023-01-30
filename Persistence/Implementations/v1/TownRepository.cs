using Microsoft.EntityFrameworkCore;
using Persistence.Context.v1;
using Persistence.Entities.v1.Towns;
using Persistence.Interfaces.v1;

namespace Persistence.Implementations.v1
{
    public class TownRepository : ITownRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TownRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<Town>> GetAllAsync()
        {
            IList<Town> towns = await _dbContext.Towns
                .Include(x => x.Vehicles)
                .ToListAsync();

            return towns;
        }

        public async Task<Town> GetByIdAsync(Guid id)
        {
            Town? town = await _dbContext.Towns
                .Include(x => x.Vehicles)
                .FirstOrDefaultAsync(x => x.Id == id);

            return town;
        }

        public async Task<Town> GetByNameAsync(string name)
        {
            Town? town = await _dbContext.Towns
                .Include(x => x.Vehicles)
                .FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());

            return town;
        }

        public async Task<bool> IsExistingTownAsync(Guid id)
        {
            return await _dbContext.Towns.AnyAsync(x => x.Id == id);
        }

    }
}
