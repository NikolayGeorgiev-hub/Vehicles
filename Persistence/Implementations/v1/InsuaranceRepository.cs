using Persistence.Context.v1;
using Persistence.Entities.v1.Insurances;
using Persistence.Interfaces.v1;

namespace Persistence.Implementations.v1
{
    public class InsuaranceRepository : IInsuaranceRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public InsuaranceRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task AddAsync(InsurancePolicy insurance)
        {
            await _applicationDbContext.AddAsync(insurance);
        }

        public async Task SaveAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
