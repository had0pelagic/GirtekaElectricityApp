using GirtekaElectricityApp.Models;

namespace GirtekaElectricityApp.Services
{
    public interface IFileReaderService
    {
        Task<List<ElectricityModel>> ReadDatasets();
    }
}
