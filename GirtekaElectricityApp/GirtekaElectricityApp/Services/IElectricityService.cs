using GirtekaElectricityApp.Models;

namespace GirtekaElectricityApp.Services
{
    public interface IElectricityService
    {
        Task<List<ElectricityModel>> GetFilteredData();
    }
}
