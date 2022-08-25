using GirtekaElectricityApp.Extensions;
using GirtekaElectricityApp.Models;
using GirtekaElectricityApp.Util.Messages;
using GirtekaElectricityInfrastructure;
using Microsoft.EntityFrameworkCore;

namespace GirtekaElectricityApp.Services
{
    public class ElectricityService : IElectricityService
    {
        private readonly Context _context;
        private readonly ILogger<ElectricityService> _logger;
        private readonly IFileReaderService _fileReaderService;

        public ElectricityService(Context context, IFileReaderService fileReaderService, ILogger<ElectricityService> logger)
        {
            _context = context;
            _fileReaderService = fileReaderService;
            _logger = logger;
        }

        /// <summary>
        /// Stores given datasets, filters data and returns filtered data gathered from database
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<ElectricityModel>> GetFilteredData()
        {
            var data = await _fileReaderService.ReadCsv();
            await StoreData(data);
            await FilterByNameAndConsumption();

            var filteredData = await _context.FilteredElectricity.ToListAsync();

            if (data.Count == 0)
            {
                throw new Exception(Message.Data.EmptyTable);
            }

            _logger.LogInformation($"Successfully found {filteredData.GetListType()} {filteredData.Count} values");

            return filteredData.Select(item => item.ToElectricityModel()).ToList();
        }

        /// <summary>
        /// Stores electricity data from given datasets
        /// </summary>
        /// <returns></returns>
        public async Task StoreData(List<ElectricityModel> data)
        {
            if (data.Count == 0)
            {
                throw new Exception(Message.Data.EmptyTable);
            }

            var mappedData = data.Select(item => item.ToElectricity()).ToList();

            foreach (var list in mappedData.Chunk(1000))
            {
                await _context.AddRangeAsync(list);
                await _context.SaveChangesAsync();
                _context.ChangeTracker.Clear();
            }

            _logger.LogInformation($"Successfully stored {mappedData.GetListType()} {mappedData.Count} values");
        }

        /// <summary>
        /// Filters electricity data by object name and below given consumption value
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="belowConsumption"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task FilterByNameAndConsumption(string objectName = "Namas", double belowConsumption = 1)
        {
            var data = await _context.Electricity.ToListAsync();

            if (data.Count == 0)
            {
                throw new Exception(Message.Data.EmptyTable);
            }

            var filteredData = data
                .Where(x => x.ObjectName == objectName && x.ElectricityConsumptionPerHour < belowConsumption)
                .Select(item => item.ToFilteredElectricity())
                .ToList();

            foreach (var list in filteredData.Chunk(1000))
            {
                await _context.AddRangeAsync(list);
                await _context.SaveChangesAsync();
                _context.ChangeTracker.Clear();
            }

            _logger.LogInformation($"Successfully filtered and stored {filteredData.GetListType()} {filteredData.Count} values");
        }

        /// <summary>
        /// Clears electricity tables
        /// </summary>
        /// <returns></returns>
        public async Task ClearElectricityData()
        {
            var electricity = await _context.Electricity.ToListAsync();
            var filteredElectricity = await _context.FilteredElectricity.ToListAsync();

            _context.Electricity.RemoveRange(electricity);
            _context.FilteredElectricity.RemoveRange(filteredElectricity);

            await _context.SaveChangesAsync();

            _logger.LogInformation($"Successfully cleared {electricity.GetListType()} and {filteredElectricity.GetListType()}");
        }
    }
}
