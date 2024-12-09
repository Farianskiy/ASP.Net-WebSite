using WebSiteElectronicMind.Core.Models;

namespace WebSiteElectronicMind.Application.Services
{
    public interface IWriteDataToExcelService
    {
        Task WriteExcelML(List<Position> predictions, string filePath);
    }
}