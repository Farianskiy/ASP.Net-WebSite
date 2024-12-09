using WebSiteElectronicMind.Core.Models;

namespace WebSiteElectronicMind.ML.Repositories
{
    public interface IReadDataFromExcelRepositories
    {
        Task<List<Position>> ReadDataFromExcelAsync(string filePath);
    }
}