using WebSiteElectronicMind.Core.Models;

namespace WebSiteElectronicMind.ML.Repositories
{
    public interface IWriteDataToExcelRepositories
    {
        Task WriteDataToExcelRepositoriesAsync(List<Position> predictions, string filePath);
    }
}