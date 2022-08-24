using GirtekaElectricityApp.Models;

namespace GirtekaElectricityApp.Services
{
    public interface IFileReaderService
    {
        List<ElectricityModel> ReadCsv();
    }
}
