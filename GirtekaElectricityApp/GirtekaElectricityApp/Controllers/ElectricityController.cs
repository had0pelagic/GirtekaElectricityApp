using GirtekaElectricityApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace GirtekaElectricityApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricityController : ControllerBase
    {
        private readonly IElectricityService _electricityService;

        public ElectricityController(IElectricityService electricityService)
        {
            _electricityService = electricityService;
        }

        [HttpGet("filtered-electricity-data")]
        public async Task<ActionResult> GetFilteredElectricityData()
        {
            return Ok("test");
        }
    }
}
