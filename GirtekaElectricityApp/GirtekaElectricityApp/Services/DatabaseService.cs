using GirtekaElectricityApp.Extensions;
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

            _context.Electricity.RemoveRange(electricity);
            _context.FilteredElectricity.RemoveRange(filteredElectricity);

            await _context.SaveChangesAsync();

            string message = $"Successfully cleared {electricity.GetListType()} and {filteredElectricity.GetListType()} tables";
            _logger.LogInformation(message);

            return message;
        }
    }
}
