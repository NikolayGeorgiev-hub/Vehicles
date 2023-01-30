using Persistence.Entities.v1.Insurances;

namespace Business.Interfaces.v1
{
    public interface IInsuaranceService
    {
        Task<InsurancePolicy> CreateAsync(Guid id);
    }
}
