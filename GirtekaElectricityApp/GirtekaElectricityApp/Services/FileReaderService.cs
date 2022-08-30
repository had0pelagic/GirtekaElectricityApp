using GirtekaElectricityApp.Extensions;
using GirtekaElectricityApp.Models;
using GirtekaElectricityApp.Util.Constants;
using GirtekaElectricityApp.Util.Messages;

namespace GirtekaElectricityApp.Services
{
    public class FileReaderService : IFileReaderService
    {
        private readonly string _datasetPath;
        private readonly ILogger<FileReaderService> _logger;

        public FileReaderService(IConfiguration configuration, ILogger<FileReaderService> logger)
        {
            _datasetPath = GetDataPath(configuration);
            _logger = logger;
        }

        /// <summary>
        /// Reads all found datasets and adds them to a new list
        /// </summary>
        /// <exception cref="Exception"></exception>
        public async Task<List<ElectricityModel>> ReadDatasets()
        {
            var filePaths = GetFilePaths();

            if (filePaths.Count == 0)
            {
                _logger.LogError(Message.File.NotFound);
                throw new Exception(Message.File.NotFound);
            }

            var list = new List<ElectricityModel>();

            foreach (var filePath in filePaths)
            {
                var data = await ReadFile(filePath);
                list.AddRange(data);
            }

            _logger.LogInformation($"Successfully read {filePaths.Count} files and created a {list.GetListType()} list containing: {list.Count} values");

            return list;
        }

        /// <summary>
        /// Reads given file and return ElectricityModel list
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<List<ElectricityModel>> ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                _logger.LogError(Message.File.NotFound);
                throw new Exception(Message.File.NotFound);
            }

            var list = new List<ElectricityModel>();
            var lines = await File.ReadAllLinesAsync(filePath);
            _logger.LogInformation($"Reading file: {filePath}");

            foreach (var line in lines.Skip(1))//skips csv heading
            {
                var data = line.Split(CsvConstants.Separator);
                list.Add(new ElectricityModel
                {
                    Region = data[0],
                    ObjectName = data[1],
                    ObjectType = data[2],
                    ObjectNumber = long.TryParse(data[3], out long numResult) ? numResult : null,
                    ElectricityConsumptionPerHour = double.TryParse(data[4], out double conResult) ? conResult : null,
                    Date = DateTime.TryParse(data[5], out DateTime date) ? date : null,
                    GeneratedElectricityPerHour = double.TryParse(data[6], out double genResult) ? genResult : null,
                });
            }

            _logger.LogInformation($"Successfully read {filePath} {list.Count} values");

            return list;
        }

        /// <summary>
        /// Returns all file paths from dataset folder
        /// </summary>
        /// <returns></returns>
        private List<string> GetFilePaths()
        {
            var directoryInfo = new DirectoryInfo(_datasetPath).GetFiles();

            return directoryInfo.Select(x => x.FullName).ToList();
        }

        /// <summary>
        /// Returns dataset folder path
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="section"></param>
        /// <param name="sectionValue"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private string GetDataPath(IConfiguration configuration, string section = "Paths", string sectionValue = "Datasets")
        {
            var pathConfig = configuration.GetSection(section);
            var path = Path.Combine(Environment.CurrentDirectory, pathConfig[sectionValue]);

            if (!Directory.Exists(path))
            {
                _logger.LogError(Message.File.NotFound);
                throw new Exception(Message.File.NotFound);
            }

            return path;
        }
    }
}
