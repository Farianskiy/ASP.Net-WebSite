using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.Net.Http;
using System.Text;
using WebSiteElectronicMind.MVC.Models.RenderingToPDF;

namespace WebSiteElectronicMind.MVC.Controllers
{
    public class RenderingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RenderingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult GeneratorPDF()
        {
            return View();
        }

        [HttpPost]
        [Route("Rendering/GeneratePdf")]
        public async Task<IActionResult> GeneratePdf()
        {
            var client = _httpClientFactory.CreateClient();

            // Сбор данных из формы
            var request = new TableFull.RenderingRequest
            {
                Shield = new TableFull.InfoShield
                {
                    NameShield = Request.Form["Shield.NameShield"],
                    FullNameShield = Request.Form["Shield.FullNameShield"],
                    TypeShield = Request.Form["Shield.TypeShield"]
                },
                Electrical = new TableFull.InfoElectrical
                {
                    NominalVoltage = int.TryParse(Request.Form["Electrical.NominalVoltage"], out var nominalVoltage) ? nominalVoltage : 0,
                    NominalShield = int.TryParse(Request.Form["Electrical.NominalShield"], out var nominalShield) ? nominalShield : 0,
                    TypeGrounding = Request.Form["Electrical.TypeGrounding"]
                },
                Cable = new TableFull.InfoCable
                {
                    SupplyCable = Request.Form["Cable.SupplyCable"],
                    CableOL = Request.Form["Cable.CableOL"]
                },
                DegreeProtection = Request.Form["DegreeProtection"],
                Omentum = new TableFull.InfoOmentum
                {
                    TypeOmentum = Request.Form["Omentum.TypeOmentum"],
                    QuantityOmentum = Request.Form["Omentum.QuantityOmentum"],
                    TypeOmentumOL = Request.Form["Omentum.TypeOmentumOL"],
                    QuantityOmentumOL = Request.Form["Omentum.QuantityOmentumOL"]
                },
                PowerCable = Request.Form["PowerCable"],
                Comment = Request.Form["Comment"],
                Build = new TableFull.InfoBuild
                {
                    FullNameEngineer = Request.Form["Build.FullNameEngineer"],
                    NumberOrderCustomer = int.TryParse(Request.Form["Build.NumberOrderCustomer"], out var numberOrderCustomer) ? numberOrderCustomer : 0,
                    NumberBuild = Request.Form["Build.NumberBuild"]
                },
                RenderingTableList = new List<TableFull.RenderingTable>()
            };

            // Сбор динамических данных RenderingTableList
            int i = 0;
            var numberBuild = Request.Form["NumberBuild"]; // Получаем номер сборки из формы
            while (Request.Form[$"RenderingTable[{i}].Name"].Any())
            {
                request.RenderingTableList.Add(new TableFull.RenderingTable
                {
                    Name = Request.Form[$"RenderingTable[{i}].Name"],
                    NameOfScheme = Request.Form[$"RenderingTable[{i}].NameOfScheme"],
                    Type = Request.Form[$"RenderingTable[{i}].Type"],
                    NumberingLetter = Request.Form[$"RenderingTable[{i}].NumberingLetter"],
                    NumberingDigit = Request.Form[$"RenderingTable[{i}].NumberingDigit"],
                    Phase = Request.Form[$"RenderingTable[{i}].Phase"],
                    Polus = int.TryParse(Request.Form[$"RenderingTable[{i}].Polus"], out var polus) ? polus : 0,
                    Level = int.TryParse(Request.Form[$"RenderingTable[{i}].Level"], out var level) ? level : 0,
                    NumberBuild = numberBuild // Присваиваем номер сборки всем объектам в списке
                });
                i++;
            }



            // Конвертация запроса в JSON
            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Отправка данных на API
            var apiUrl = "http://localhost:7014/RenderingControllers/generator-Pdf";
            var response = await client.PostAsync(apiUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, new { Message = "Error: " + errorMessage });
            }

            var result = await response.Content.ReadAsAsync<dynamic>();

            string filePath = result.path;

            var downloadResponse = await client.GetAsync($"http://localhost:7014/Position/download-files?filePath={Uri.EscapeDataString(filePath)}");

            if (!downloadResponse.IsSuccessStatusCode)
            {
                var errorMessage = await downloadResponse.Content.ReadAsStringAsync();
                return StatusCode((int)downloadResponse.StatusCode, new { Message = "Error: " + errorMessage });
            }

            var fileBytes = await downloadResponse.Content.ReadAsByteArrayAsync();

            return File(fileBytes, "application/pdf", "output.pdf");

        }

        [HttpPost("UploadExcel")]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { Message = "Файл не загружен или пуст." });

            try
            {
                var validResults = new List<string>();
                string buildNumber = string.Empty; // Для хранения номера сборки

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;

                    using (var workbook = new ClosedXML.Excel.XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheets.FirstOrDefault();
                        if (worksheet == null)
                            return BadRequest(new { Message = "Невозможно прочитать Excel файл." });

                        // Читаем вторую строку второго столбца для номера сборки
                        var orderInfo = worksheet.Cell(2, 2).GetValue<string>(); // 2-й столбец, 2-я строка
                        if (!string.IsNullOrEmpty(orderInfo))
                        {
                            var match = System.Text.RegularExpressions.Regex.Match(orderInfo, @"№\s*(\d+)");
                            if (match.Success)
                            {
                                buildNumber = match.Groups[1].Value; // Извлекаем номер сборки
                            }
                        }

                        // Начинаем чтение с 10 строки, столбец 4 (D)
                        int row = 10;
                        while (true)
                        {
                            var cellValue = worksheet.Cell(row, 4).GetValue<string>(); // 4-й столбец
                            if (string.IsNullOrEmpty(cellValue)) break; // Прерываем, если ячейка пуста

                            // Проверяем через API, является ли оборудование допустимым
                            var apiResponse = await GetNameFromApiInternal(cellValue);
                            if (!string.IsNullOrEmpty(apiResponse))
                            {
                                validResults.Add(apiResponse);
                            }

                            row++;
                        }
                    }
                }

                return Ok(new { names = validResults, buildNumber }); // Возвращаем данные и номер сборки
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }


        // Вспомогательный метод для вызова API
        private async Task<string> GetNameFromApiInternal(string name)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync($"http://localhost:7014/RenderingControllers/api/GetNames?name={Uri.EscapeDataString(name)}");

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null; // Пропускаем недопустимое оборудование
                }

                if (!response.IsSuccessStatusCode)
                {
                    return $"Ошибка API: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
                }

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return null; // Игнорируем оборудование с ошибками
            }
        }












    }
}
