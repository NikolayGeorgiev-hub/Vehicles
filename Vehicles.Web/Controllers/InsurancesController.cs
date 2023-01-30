using Business.Interfaces.v1;
using Microsoft.AspNetCore.Mvc;

namespace Vehicles.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsurancesController : ControllerBase
    {
        private readonly IInsuaranceService _insuaranceService;

        public InsurancesController(IInsuaranceService insuaranceService)
        {
            _insuaranceService = insuaranceService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> Get(Guid id)
        {
            try
            {
                var insurance = await _insuaranceService.CreateAsync(id);
                return this.Ok(insurance);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
