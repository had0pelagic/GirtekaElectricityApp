using GirtekaElectricityApp.Models;
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

        /// <summary>
        /// Stores given datasets, filters data and returns filtered data gathered from database
        /// </summary>
        /// <returns></returns>
        [HttpGet("filtered-data")]
        public async Task<ActionResult<List<ElectricityModel>>> GetFilteredElectricityData()
        {
            var data = await _electricityService.GetFilteredData();

            return Ok(data);
        }
    }
}
