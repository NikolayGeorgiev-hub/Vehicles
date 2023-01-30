using Persistence.Entities.v1.Insurances;

namespace Persistence.Interfaces.v1
{
    public interface IInsuaranceRepository
    {
        Task AddAsync(InsurancePolicy insurance);

        Task SaveAsync();

    }
}
