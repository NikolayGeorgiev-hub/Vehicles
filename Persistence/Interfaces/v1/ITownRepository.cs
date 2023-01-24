using Persistence.Entities.v1;
using System.Threading.Tasks;

namespace Persistence.Interfaces.v1
{
    public interface ITownRepository
    {
        Task<IList<Town>> GetAllAsync();

        Task<Town> GetByNameAsync(string name);

        Task<Town> GetByIdAsync(Guid id);

        Task<bool> IsExistingTownAsync(Guid id);
    }
}
