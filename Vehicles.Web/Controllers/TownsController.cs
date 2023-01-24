using Business.Interfaces.v1;
using Business.Models.v1;
using Microsoft.AspNetCore.Mvc;

namespace Vehicles.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TownsController : ControllerBase
    {
        private readonly ITownService _townService;
        
        public TownsController(ITownService townService)
        {
            _townService = townService;
        }


        [HttpGet]
        public async Task<IEnumerable<TownResponse>> GetAllTowns()
        {
            var towns = await _townService.GetAllAsync();
            return towns;
        }

        [HttpGet("{name}")]
        public async Task<TownResponse> ByName(string name)
        {
            var town = await _townService.GetByNameAsync(name);
            return town;
        }

        [HttpGet("{id:guid}")]
        public async Task<TownResponse> ById(Guid id)
        {
            var town = await _townService.GetByIdAsync(id);
            return town;
        }
    }
}
