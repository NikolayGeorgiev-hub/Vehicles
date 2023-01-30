using Persistence.Entities.v1;

namespace Business.Interfaces.v1
{
    public interface IInsuaranceService
    {
        Task<InsurancePolicy> CreateAsync(Guid id);
    }
}
