using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using WebSiteElectronicMind.Core.Models.RenderingToPDF;

namespace WebSiteElectronicMind.Application.Services.RenderingPdf
{
    public class HistoryPdfService
    {
        private readonly string _historyFilePath =
            Path.Combine(Directory.GetCurrentDirectory(), "Files/RenderingPDF/History/history.json");

        public async Task SaveHistoryAsync(List<TablePDF> tablePDFs)
        {
            // Загружаем существующий файл истории (если есть)
            var existingHistory = await LoadHistoryAsync();

            // Группируем данные по `NumberBuild`
            foreach (var table in tablePDFs)
            {
                // Если ключ уже существует, заменяем полностью данные для этого NumberBuild
                if (existingHistory.ContainsKey(table.NumberBuild))
                {
                    existingHistory[table.NumberBuild] = tablePDFs
                        .Where(t => t.NumberBuild == table.NumberBuild)
                        .ToList();
                }
                else
                {
                    // Если ключ отсутствует, добавляем новую запись
                    existingHistory[table.NumberBuild] = new List<TablePDF>
                    {
                        table
                    };
                }
            }

            // Опции для сохранения строки с читаемыми символами
            var jsonData = JsonSerializer.Serialize(existingHistory, new JsonSerializerOptions
            {
                WriteIndented = true, // Для читаемости JSON
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) // Сохраняет символы без экранирования
            });

            await File.WriteAllTextAsync(_historyFilePath, jsonData);
        }

        public async Task<Dictionary<string, List<TablePDF>>> LoadHistoryAsync()
        {
            if (!File.Exists(_historyFilePath))
            {
                return new Dictionary<string, List<TablePDF>>();
            }

            var jsonData = await File.ReadAllTextAsync(_historyFilePath);
            return JsonSerializer.Deserialize<Dictionary<string, List<TablePDF>>>(jsonData)
                ?? new Dictionary<string, List<TablePDF>>();
        }

        public async Task<Dictionary<string, List<TablePDF>>> GetLastTenHistoryRecordsAsync()
        {
            var history = await LoadHistoryAsync();  // Загружаем все записи истории

            // Ограничиваем результат 10 последними записями
            return history.OrderByDescending(kv => kv.Key)  // Сортировка по ключу (например, по номеру сборки или дате)
                          .Take(10) // Берем только 10 последних записей
                          .ToDictionary(kv => kv.Key, kv => kv.Value);
        }

    }
}
