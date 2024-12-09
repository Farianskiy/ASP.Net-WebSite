using CSharpFunctionalExtensions;
using WebSiteElectronicMind.ML.Repositories;
using WebSiteElectronicMind.Core.Models;

namespace WebSiteElectronicMind.Application.Services
{
    public class ReadDataFromExcelService : IReadDataFromExcelService
    {
        private readonly IReadDataFromExcelRepositories _readDataFromExcelRepositories;

        public ReadDataFromExcelService(IReadDataFromExcelRepositories readDataFromExcelRepositories)
        {
            _readDataFromExcelRepositories = readDataFromExcelRepositories;
        }

        public async Task<Result<List<Position>>> ReadExcelML(string filePath)
        {
            var positions = await _readDataFromExcelRepositories.ReadDataFromExcelAsync(filePath);
            return Result.Success(positions);
        }
    }
}
