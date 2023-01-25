using Business.Interfaces.v1;
using Business.Models.v1.Vehicles;
using Microsoft.AspNetCore.Mvc;

namespace Vehicles.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost]
        public async Task<VehicleResponse> CreateAsync(VehicleRequest requestVehicle)
        {
            var result = await _vehicleService.CreateAsync(requestVehicle);
            return result;
        }

        [HttpPut("{Id:guid}")]
        public async Task<UpdateResponse> UpdateVehicle(Guid Id, VehicleRequest editModel)
        {
            var result = await _vehicleService.UpdateAsync(Id, editModel);
            return result;
        }


        [HttpGet]
        public async Task<IEnumerable<VehicleResponse>> GetAllAsync()
        {
            var vehicles = await _vehicleService.GetAllAsync();
            return vehicles;
        }

        [HttpGet("{townName}")]
        public async Task<IEnumerable<VehicleResponse>> GetByTownAsync(string townName)
        {
            var vehicles = await _vehicleService.GetByTownAsync(townName);
            return vehicles;
        }

        [HttpGet("Range/{min}/{max}")]
        public async Task<IEnumerable<VehicleResponse>> GetInRange(int min, int max)
        {
            var result = await _vehicleService.GetVehiclesInRangeAsync(min, max);
            return result;
        }

        [HttpGet("{Id:guid}")]
        public async Task<VehicleResponse> GetById(Guid Id)
        {
            var vehicle = await _vehicleService.GetByIdAsync(Id);
            return vehicle;
        }

        [HttpDelete("{Id:guid}")]
        public async Task<DeleteRespons> RemoveVehicleAsync(Guid Id)
        {
            var result = await _vehicleService.DeleteAsync(Id);
            return result;
        }

    }
}
