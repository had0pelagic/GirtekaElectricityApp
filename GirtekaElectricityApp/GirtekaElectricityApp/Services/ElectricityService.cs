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
        public async Task<List<FilteredElectricityModel>> GetFilteredData()
        {
            var filteredData = await _context.FilteredElectricity.ToListAsync();

            if (filteredData.Count == 0)
            {
                _logger.LogError(Message.Data.EmptyList);
                throw new Exception(Message.Data.EmptyList);
            }

            _logger.LogInformation($"Found {filteredData.GetListType()} {filteredData.Count} values");

            return filteredData.Select(item => item.ToFilteredElectricityModel()).ToList();
        }

        /// <summary>
        /// Stores electricity data from given datasets
        /// </summary>
        /// <returns></returns>
        public async Task<string> StoreElectricityData()
        {
            var data = await _fileReaderService.ReadDatasets();

            if (data.Count == 0)
            {
                _logger.LogError(Message.Data.EmptyList);
                throw new Exception(Message.Data.EmptyList);
            }

            var mappedData = data.Select(item => item.ToElectricity()).ToList();

            foreach (var list in mappedData.Chunk(1000))
            {
                await _context.AddRangeAsync(list);
                await _context.SaveChangesAsync();
                _context.ChangeTracker.Clear();
            }

            var message = $"Successfully stored {mappedData.GetListType()} {mappedData.Count} values";
            _logger.LogInformation(message);

            return message;
        }

        /// <summary>
        /// Filters electricity data by object name and below given consumption value
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="belowConsumption"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> FilterElectricityData(string objectName = "Namas", double belowConsumption = 1)
        {
            var data = await _context.Electricity.ToListAsync();

            if (data.Count == 0)
            {
                _logger.LogError(Message.Data.EmptyList);
                throw new Exception(Message.Data.EmptyList);
            }

            var filteredData = data
                .Where(x => x.ObjectName == objectName && x.ElectricityConsumptionPerHour < belowConsumption)
                .Select(item => item.ToFilteredElectricity())
                .ToList();

            _logger.LogInformation($"After filtering with objectName: {objectName} and consumption: {belowConsumption} " +
                                   $"found {filteredData.Count} values out of original {data.Count} values");

            foreach (var list in filteredData.Chunk(1000))
            {
                await _context.AddRangeAsync(list);
                await _context.SaveChangesAsync();
                _context.ChangeTracker.Clear();
            }

            var message = $"Successfully filtered and stored {filteredData.GetListType()} {filteredData.Count} values";
            _logger.LogInformation(message);

            return message;
        }
    }
}
