using GirtekaElectricityApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace GirtekaElectricityApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricityController : ControllerBase
    {
        private readonly IElectricityService _electricityService;
        private readonly IFileReaderService _fileRead;

        public ElectricityController(IElectricityService electricityService, IFileReaderService fileRead)
        {
            _electricityService = electricityService;
            _fileRead = fileRead;
        }

        [HttpGet("filtered-electricity-data")]
        public async Task<ActionResult> GetFilteredElectricityData()
        {
            var list = _fileRead.ReadCsv();
            return Ok("test");
        }
    }
}
