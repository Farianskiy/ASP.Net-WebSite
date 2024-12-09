using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using WebSiteElectronicMind.Core.Models;

namespace WebSiteElectronicMind.ML.Repositories
{
    public class ReadDataFromExcelRepositories : IReadDataFromExcelRepositories
    {
        private readonly ILogger<ReadDataFromExcelRepositories> _logger;

        public ReadDataFromExcelRepositories(ILogger<ReadDataFromExcelRepositories> logger)
        {
            _logger = logger;
        }

        public async Task<List<Position>> ReadDataFromExcelAsync(string filePath)
        {
            var inputData = new List<Position>();
            try
            {
                var fileInfo = new FileInfo(filePath);
                using (var package = new ExcelPackage(fileInfo))
                {
                    await package.LoadAsync(fileInfo); // Асинхронная загрузка файла

                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    var rows = Enumerable.Range(2, rowCount - 1).ToArray(); // Создаем массив строк для обработки

                    var parallelOptions = new ParallelOptions
                    {
                        MaxDegreeOfParallelism = 4 // Настраиваем количество потоков
                    };

                    // Параллельная обработка строк
                    Parallel.ForEach(rows, parallelOptions, row =>
                    {
                        try
                        {
                            var code = worksheet.Cells[row, 1].Text;
                            var article = worksheet.Cells[row, 2].Text;
                            var name = worksheet.Cells[row, 3].Text;

                            var positionResult = Position.Create(code, article, name, string.Empty, new Dictionary<string, string>());

                            if (positionResult.IsSuccess)
                            {
                                lock (inputData) // Блокировка для потокобезопасного доступа
                                {
                                    inputData.Add(positionResult.Value);
                                }
                            }
                            else
                            {
                                _logger.LogError($"Ошибка создания позиции: {positionResult.Error}");
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"Ошибка обработки строки {row}: {ex.Message}");
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при чтении данных из Excel: {ex.Message}");
            }
            return inputData;
        }
    }
}
