using AutoMapper;
using Business.Interfaces.v1;
using Business.Models.v1.Towns;
using Persistence.Interfaces.v1;

namespace Business.Implementations.v1
{
    public class TownService : ITownService
    {
        private readonly ITownRepository _townRepository;
        private readonly IMapper _mapper;
        
        public TownService(ITownRepository townRepository, IMapper mapper)
        {
            _townRepository = townRepository;
            _mapper = mapper;
        }

        public async Task<IList<TownResponse>> GetAllAsync()
        {
            var towns = await _townRepository.GetAllAsync();
            var allTowns = _mapper.Map<IEnumerable<TownResponse>>(towns).ToList();

            return allTowns;
        }

        public async Task<TownResponse> GetByNameAsync(string name)
        {
            var town = await _townRepository.GetByNameAsync(name);
            var currentTown = _mapper.Map<TownResponse>(town);

            return currentTown;
        }

        public async Task<TownResponse> GetByIdAsync(Guid id)
        {
            var town = await _townRepository.GetByIdAsync(id);
            var townWithCars = _mapper.Map<TownResponse>(town);

            return townWithCars;
        }

    }
}
