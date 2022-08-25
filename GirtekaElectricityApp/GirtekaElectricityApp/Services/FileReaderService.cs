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
        /// Reads all found datasets
        /// </summary>
        /// <exception cref="Exception"></exception>
        public async Task<List<ElectricityModel>> ReadCsv()
        {
            var filePaths = GetFilePaths();

            if (filePaths.Count == 0)
            {
                throw new Exception(Message.File.NotFound);
            }

            var list = new List<ElectricityModel>();

            foreach (var file in filePaths)
            {
                var lines = await File.ReadAllLinesAsync(file);
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
            }

            _logger.LogInformation($"Successfully read and created a {list.GetListType()} list containing: {list.Count} values");

            return list;
        }

        /// <summary>
        /// Returns all file names from
        /// </summary>
        /// <returns></returns>
        private List<string> GetFilePaths()
        {
            var directoryInfo = new DirectoryInfo(_datasetPath).GetFiles();

            return directoryInfo.Select(x => x.FullName).ToList();
        }

        /// <summary>
        /// Returns path of the folder which contains datasets
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="section"></param>
        /// <param name="sectionValue"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static string GetDataPath(IConfiguration configuration, string section = "Paths", string sectionValue = "Datasets")
        {
            var pathConfig = configuration.GetSection(section);
            var path = Path.Combine(Environment.CurrentDirectory, pathConfig[sectionValue]);

            if (!Directory.Exists(path))
            {
                throw new Exception(Message.File.NotFound);
            }

            return path;
        }
    }
}
