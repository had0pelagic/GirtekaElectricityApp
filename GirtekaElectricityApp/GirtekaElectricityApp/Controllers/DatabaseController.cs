using GirtekaElectricityApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace GirtekaElectricityApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;

        public DatabaseController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// Clears electricity tables
        /// </summary>
        /// <returns></returns>
        [HttpGet("clear-electricity")]
        public async Task<ActionResult<string>> ClearElectricityData()
        {
            return Ok(await _databaseService.ClearElectricityData());
        }
    }
}
