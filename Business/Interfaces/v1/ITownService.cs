using Business.Models.v1.Towns;

namespace Business.Interfaces.v1
{
    public interface ITownService
    {
        Task<IList<TownResponse>> GetAllAsync();

        Task<TownResponse> GetByNameAsync(string name);

        Task<TownResponse> GetByIdAsync(Guid id);
    }
}
