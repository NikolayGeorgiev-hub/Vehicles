using Business.Interfaces.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vehicles.Data.Interfaces.v1;

namespace Vehicles.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IInsuaranceService insuaranceService;
        private readonly IVehicleRepository vehicleRepository;

        public TestController(IInsuaranceService insuaranceService, IVehicleRepository vehicleRepository)
        {
            this.insuaranceService = insuaranceService;
            this.vehicleRepository = vehicleRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var id = new Guid("D772A430-0F7A-4881-9056-5D731DEA0643");
            var vehicle = await vehicleRepository.GetByIdAsync(id);

            var aa = this.insuaranceService.Create(vehicle);
            return this.Ok(aa);
        }

    }
}
