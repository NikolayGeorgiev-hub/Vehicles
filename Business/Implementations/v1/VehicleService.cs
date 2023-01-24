using Vehicles.Data.Interfaces.v1;
using AutoMapper;
using Business.Models.v1;
using Business.Interfaces.v1;
using Persistence.Entities.v1;
using Persistence.Interfaces.v1;

using static Business.Constants;

namespace Business.Implementations.v1
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ITownRepository _townRepository;
        private readonly IMapper _mapper;

        public VehicleService(IVehicleRepository vehicleRepository, ITownRepository townRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _townRepository = townRepository;
            _mapper = mapper;
        }

        public async Task<VehicleResponse> CreateAsync(VehicleRequest model)
        {
            var vehicle = new Vehicle
            {
                EngineCapacity = model.EngineCapacity,
                VehicleAge = model.VehicleAge,
                Purpose = model.Purpose,
                VehicleTypeId = model.VehicleTypeId,
                TownId = model.TownId,
            };

            if (await _townRepository.IsExistingTownAsync(vehicle.TownId))
            {
                _vehicleRepository.Add(vehicle);
                await _vehicleRepository.SaveChangesAsync();
            }

            var vehicleResponse = _mapper.Map<VehicleResponse>(await _vehicleRepository.GetByIdAsync(vehicle.Id));
            return vehicleResponse;

        }

        public async Task<DeleteRespons> DeleteAsync(Guid id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            var deleteResponse = new DeleteRespons();

            if (vehicle != null)
            {
                _vehicleRepository.Delete(vehicle);
                await _vehicleRepository.SaveChangesAsync();

                deleteResponse = _mapper.Map<DeleteRespons>(vehicle);
                deleteResponse.IsDelited = true;
                deleteResponse.Reason = SuccessfulRemove;
            }
            else
            {
                deleteResponse.Reason = NotFoundVehicle;
            }

            return deleteResponse;
        }


        public async Task<IList<VehicleResponse>> GetAllAsync()
        {
            var vehicles = await _vehicleRepository.GetAllAsync();
            var vehiclesResponse = _mapper.Map<IList<VehicleResponse>>(vehicles);

            return vehiclesResponse;
        }

        public async Task<VehicleResponse> GetByIdAsync(Guid id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            var response = _mapper.Map<VehicleResponse>(vehicle);

            return response;
        }

        public async Task<IList<VehicleResponse>> GetVehiclesInRangeAsync(int min, int max)
        {
            var vehicles = await _vehicleRepository.GetAllAsync();

            var vehiclesInRange = vehicles
                .Where(x => x.VehicleAge >= min && x.VehicleAge <= max)
                .OrderByDescending(x => x.VehicleAge).ToList();

            var response = _mapper.Map<IList<VehicleResponse>>(vehiclesInRange);
            return response;
        }

        public async Task<IList<VehicleResponse>> GetByTownAsync(string townName)
        {
            var vehicles = await _vehicleRepository.GetAllAsync();
            var vehiclesByTown = vehicles
                .Where(x => x.Town.Name.ToLower() == townName.ToLower());

            var response = _mapper.Map<IList<VehicleResponse>>(vehiclesByTown);
            return response;
        }

        public Task<VehicleResponse> ThrowException()
        {
            throw new NotImplementedException();
        }

        public async Task<UpdateResponse> UpdateAsync(Guid id, VehicleRequest model)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            var updateResponse = new UpdateResponse();

            if (vehicle != null)
            {
                vehicle.EngineCapacity = model.EngineCapacity;
                vehicle.VehicleAge = model.VehicleAge;
                vehicle.TownId = model.TownId;
                vehicle.VehicleTypeId = model.VehicleTypeId;
                vehicle.Purpose = model.Purpose;

                _vehicleRepository.Update(vehicle);
                await _vehicleRepository.SaveChangesAsync();

                updateResponse = _mapper.Map<UpdateResponse>(vehicle);
                updateResponse.IsUpdated = true;
                updateResponse.Reason = SuccessfulUpdate;
            }
            else
            {
                updateResponse.Reason = NotFoundVehicle;
            }

            return updateResponse;
        }
    }
}
