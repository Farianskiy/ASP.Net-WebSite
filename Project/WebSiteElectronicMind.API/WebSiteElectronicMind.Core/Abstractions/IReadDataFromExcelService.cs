using CSharpFunctionalExtensions;
using WebSiteElectronicMind.Core.Models;

namespace WebSiteElectronicMind.Application.Services
{
    public interface IReadDataFromExcelService
    {
        Task<Result<List<Position>>> ReadExcelML(string filePath);
    }
}