using GirtekaElectricityApp.Extensions;
using GirtekaElectricityDomain;
using GirtekaElectricityInfrastructure;
using Microsoft.EntityFrameworkCore;

namespace GirtekaElectricityApp.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly Context _context;
        private readonly ILogger<DatabaseService> _logger;

        public DatabaseService(Context context, ILogger<DatabaseService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Clears electricity tables
        /// </summary>
        /// <returns></returns>
        public async Task<string> ClearElectricityData()
        {
            var electricity = await _context.Electricity.ToListAsync();
            var filteredElectricity = await _context.FilteredElectricity.ToListAsync();

            await RemoveData(electricity);
            await RemoveData(filteredElectricity);

            string message = $"Successfully cleared {electricity.GetListType()} and {filteredElectricity.GetListType()} tables";
            _logger.LogInformation(message);

            return message;
        }

        /// <summary>
        /// Removes given data in chunks
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task RemoveData<T>(IList<T> data) where T : class
        {
            foreach (var list in data.Chunk(1000))
            {
                _context.Set<T>().RemoveRange(list);
                await _context.SaveChangesAsync();
                _context.ChangeTracker.Clear();
            }
        }
    }
}
