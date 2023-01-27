using Business.Interfaces.v1;
using Business.Models.v1.Vehicles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Persistence.Entities.v1;

using static Business.Constants;

namespace Vehicles.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = ApplicationRoles.OwnerRoleName)]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly UserManager<ApplicationUser> _userManager;

        public VehicleController(
            IVehicleService vehicleService,
            UserManager<ApplicationUser> userManager)
        {
            _vehicleService = vehicleService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<VehicleResponse> CreateAsync(VehicleRequest requestVehicle)
        {
            var user = await _userManager.GetUserAsync(this.User);
            try
            {
                var response = await _vehicleService.CreateAsync(requestVehicle, user.Id);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{Id:guid}")]
        public async Task<UpdateResponse> UpdateVehicle(Guid Id, VehicleRequest editModel)
        {
            var user = await _userManager.GetUserAsync(this.User);
            try
            {
                var response = await _vehicleService.UpdateAsync(Id, user.Id, editModel);
                return response;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        [HttpDelete("{Id:guid}")]
        public async Task<DeleteRespons> RemoveVehicleAsync(Guid Id)
        {
            var user = await _userManager.GetUserAsync(this.User);
            try
            {
                var response = await _vehicleService.DeleteAsync(Id, user.Id);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


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


    }
}
