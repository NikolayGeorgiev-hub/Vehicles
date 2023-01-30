using Vehicles.Data.Interfaces.v1;
using AutoMapper;
using Business.Interfaces.v1;
using Persistence.Interfaces.v1;
using Business.Models.v1.Vehicles;
using Microsoft.Extensions.Logging;
using Business.Models.v1.Errors;
using System.Net;

using static Business.Constants;
using Persistence.Entities.v1.Vehicles;

namespace Business.Implementations.v1
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ITownRepository _townRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<VehicleService> _logger;

        public VehicleService(
            IVehicleRepository vehicleRepository,
            ITownRepository townRepository,
            IMapper mapper, ILogger<VehicleService> logger)
        {
            _vehicleRepository = vehicleRepository;
            _townRepository = townRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<VehicleResponse> CreateAsync(VehicleRequest vehicleRequest, Guid ownerId)
        {
            bool existingTown = await _townRepository.IsExistingTownAsync(vehicleRequest.TownId);
            bool validType = await _vehicleRepository.IsValidVehicleTypeAsync(vehicleRequest.VehicleTypeId);

            if (existingTown && validType)
            {
                var vehicle = new Vehicle
                {
                    EngineCapacity = vehicleRequest.EngineCapacity,
                    VehicleAge = vehicleRequest.VehicleAge,
                    Purpose = vehicleRequest.Purpose,
                    VehicleTypeId = vehicleRequest.VehicleTypeId,
                    TownId = vehicleRequest.TownId,
                    OwnerId = ownerId
                };

                _vehicleRepository.Add(vehicle);
                await _vehicleRepository.SaveChangesAsync();

                var vehicleResponse = _mapper.Map<VehicleResponse>(await _vehicleRepository.GetByIdAsync(vehicle.Id));
                vehicleResponse.IsSucceeded = true;
                _logger.Log(LogLevel.Information, string.Format(LoggerMessages.SuccessfulAddNewVehicle, vehicleResponse.Purpose));

                return vehicleResponse;
            }

            var messageParameter = existingTown is false ?
                $"town {vehicleRequest.TownId}" : $"type {vehicleRequest.VehicleTypeId}";

            var message = string.Format(LoggerMessages.NotFound, messageParameter);
            var vehicleErrorResponse = new VehicleResponse
            {
                IsSucceeded = false,
                Error = new Error { StatusCode = HttpStatusCode.NotFound, Description = message }
            };

            _logger.Log(LogLevel.Error, message);
            return vehicleErrorResponse;

        }

        public async Task<UpdateResponse> UpdateAsync(Guid vehicleId, Guid ownerId, VehicleRequest model)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);

            if (vehicle is null)
            {
                _logger.Log(LogLevel.Error, string.Format(LoggerMessages.NotFound, "vehicle"));
                var updateResponse = new UpdateResponse
                {
                    IsSucceeded = false,
                    Error = new Error
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Description = string.Format(VehicleMessages.NotFound, vehicleId)
                    }
                };
                return updateResponse;
            }

            if (vehicle.OwnerId != ownerId)
            {
                _logger.Log(LogLevel.Error, string.Format(LoggerMessages.ForbiddenAction, "update"));
                var updateResponse = new UpdateResponse
                {
                    IsSucceeded = false,
                    Error = new Error
                    {
                        StatusCode = HttpStatusCode.Forbidden,
                        Description = VehicleMessages.Forbidden,
                    }
                };
                return updateResponse;
            }

            var vehicleTownId = vehicle.Town.Id;

            vehicle.EngineCapacity = model.EngineCapacity;
            vehicle.VehicleAge = model.VehicleAge;
            vehicle.TownId = model.TownId;
            vehicle.VehicleTypeId = model.VehicleTypeId;
            vehicle.Purpose = model.Purpose;

            await _vehicleRepository.UpdateByIdAsync(vehicle.Id);
            await _vehicleRepository.SaveChangesAsync();

            if (vehicleTownId != vehicle.TownId)
            {
                var town = await _townRepository.GetByIdAsync(vehicle.TownId);
                vehicle.Town = town;
            }

            var response = new UpdateResponse();
            response = _mapper.Map<UpdateResponse>(vehicle);
            response.IsSucceeded = true;

            _logger.Log(LogLevel.Information, LoggerMessages.SuccessfulUpdate);
            return response;
        }

        public async Task<DeleteRespons> DeleteAsync(Guid id, Guid ownerId)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);

            if (vehicle is null)
            {
                _logger.Log(LogLevel.Error, string.Format(LoggerMessages.NotFound, "vehicle"));
                var deleteResponse = new DeleteRespons
                {
                    IsSucceeded = false,
                    Error = new Error
                    {
                        StatusCode = HttpStatusCode.NotFound,
                        Description = string.Format(VehicleMessages.NotFound, id)
                    }
                };
                return deleteResponse;
            }

            if (vehicle.OwnerId != ownerId)
            {
                _logger.Log(LogLevel.Error, string.Format(LoggerMessages.ForbiddenAction, "delete"));
                var deleteResponse = new DeleteRespons
                {
                    IsSucceeded = false,
                    Error = new Error
                    {
                        StatusCode = HttpStatusCode.Forbidden,
                        Description = VehicleMessages.Forbidden,
                    }
                };
                return deleteResponse;
            }

            await _vehicleRepository.DeleteByIdAsync(vehicle.Id);
            await _vehicleRepository.SaveChangesAsync();

            var response = new DeleteRespons();
            response = _mapper.Map<DeleteRespons>(vehicle);
            response.IsSucceeded = true;

            _logger.Log(LogLevel.Information, LoggerMessages.SuccessfulRemove);
            return response;
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


    }
}
