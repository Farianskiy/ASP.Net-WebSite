using WebSiteElectronicMind.Core.Models;
using WebSiteElectronicMind.ML.Repositories;

namespace WebSiteElectronicMind.Application.Services
{
    public class WriteDataToExcelService : IWriteDataToExcelService
    {
        private readonly IWriteDataToExcelRepositories _writeDataToExcelRepositories;

        public WriteDataToExcelService(IWriteDataToExcelRepositories writeDataToExcelRepositories)
        {
            _writeDataToExcelRepositories = writeDataToExcelRepositories;
        }

        public async Task WriteExcelML(List<Position> predictions, string filePath)
        {
            await _writeDataToExcelRepositories.WriteDataToExcelRepositoriesAsync(predictions, filePath);
        }
    }
}
