namespace GirtekaElectricityApp.Services
{
    public interface IDatabaseService
    {
        Task<string> ClearElectricityData();
    }
}
