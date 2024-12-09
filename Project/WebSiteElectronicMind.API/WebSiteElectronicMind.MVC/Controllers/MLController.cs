using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using WebSiteElectronicMind.MVC.Models;

namespace WebSiteElectronicMind.MVC.Controllers
{
    public class MLController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MLController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> PredictionML()
        {
            var latestFiles = await GetLatestFiles();
            var newFile = latestFiles.FirstOrDefault();
            var historyFiles = latestFiles.Skip(1).ToList();
            ViewBag.NewFile = newFile;
            return View(historyFiles);
        }

        [HttpPost]
        public async Task<IActionResult> PredictionML(string TitelFileName, IFormFile FileExcel)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(TitelFileName), "TitelFileName");

            if (FileExcel != null && FileExcel.Length > 0)
            {
                var fileContent = new StreamContent(FileExcel.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(FileExcel.ContentType);
                content.Add(fileContent, "FileExcel", FileExcel.FileName);
            }

            var response = await client.PostAsync("http://localhost:7014/Position", content); // Укажите адрес вашего API

            if (response.IsSuccessStatusCode)
            {
                var latestFiles = await GetLatestFiles();
                var newFile = latestFiles.FirstOrDefault();
                var historyFiles = latestFiles.Skip(1).ToList();
                ViewBag.NewFile = newFile;
                return View(historyFiles);
            }

            return View();
        }

        [HttpGet]
        public async Task<List<FileMetadata>> GetLatestFiles()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://localhost:7014/Position/latest-files");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var latestFiles = JsonSerializer.Deserialize<List<FileMetadata>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    // Если десериализация вернула null, то возвращаем пустой список
                    return latestFiles ?? new List<FileMetadata>();
                }
                else
                {
                    return new List<FileMetadata>();
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> DownloadFile(string filePath)
        {
            var client = _httpClientFactory.CreateClient();
            // Корректный вызов API с передачей параметра filePath через FromQuery
            var response = await client.GetAsync($"http://localhost:7014/Position/download-files?filePath={Uri.EscapeDataString(filePath)}");

            if (response.IsSuccessStatusCode)
            {
                var fileBytes = await response.Content.ReadAsByteArrayAsync();
                var fileName = Path.GetFileName(filePath);
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            return NotFound();
        }






    }
}
