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
            var result = await _electricityService.GetFilteredData();

            return Ok(result);
        }

        /// <summary>
        /// Stores electricity data from given datasets
        /// </summary>
        /// <returns></returns>
        [HttpGet("store-electricity-data")]
        public async Task<ActionResult<string>> StoreElectricityData()
        {
            var result = await _electricityService.StoreElectricityData();

            return Ok(result);
        }

        /// <summary>
        /// Filters electricity data by object name and below given consumption value
        /// </summary>
        /// <returns></returns>
        [HttpGet("filter-electricity-data")]
        public async Task<ActionResult<string>> FilterElectricityData()
        {
            var result = await _electricityService.FilterElectricityData();

            return Ok(result);
        }
    }
}
