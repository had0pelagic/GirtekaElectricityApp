using GirtekaElectricityApp.Models;

namespace GirtekaElectricityApp.Services
{
    public interface IElectricityService
    {
        Task<List<FilteredElectricityModel>> GetFilteredData();
        Task<string> StoreElectricityData();
        Task<string> FilterElectricityData(string objectName = "Namas", double belowConsumption = 1);
    }
}
