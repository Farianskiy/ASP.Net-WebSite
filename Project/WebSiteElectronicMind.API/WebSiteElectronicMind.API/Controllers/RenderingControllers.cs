using Microsoft.AspNetCore.Mvc;
using WebSiteElectronicMind.API.Contracts;
using WebSiteElectronicMind.Application.Services.RenderingPdf;
using WebSiteElectronicMind.Core.Models.RenderingToPDF;
using WebSiteElectronicMind.ML.Repositories;
using WebSiteElectronicMind.Rendering.Repositories;
using PdfModels = WebSiteElectronicMind.Core.Models.RenderingToPDF.Subclasses;


namespace WebSiteElectronicMind.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RenderingControllers : ControllerBase
    {
        private readonly string _filesPdfOutput =
            Path.Combine(Directory.GetCurrentDirectory(), "Files/PDF");

        private readonly GeneratorPdfService _generatorPdfService;
        private readonly ISchemeDetailsRepositories _schemeDetailsRepositories;
        private readonly HistoryPdfService _historyPdfService;

        public RenderingControllers(GeneratorPdfService generatorPdfService, ISchemeDetailsRepositories schemeDetailsRepositories, HistoryPdfService historyPdfService)
        {
            _generatorPdfService = generatorPdfService;
            _schemeDetailsRepositories = schemeDetailsRepositories;
            _historyPdfService = historyPdfService;
        }

        [HttpPost("generator-Pdf")]
        public async Task<IActionResult> GeneratePdf1([FromBody]RenderingRequest request)
        {
            var table1C = Table1C.Create(
                PdfModels.InfoShield.Create(request.Shield.NameShield, request.Shield.FullNameShield, request.Shield.TypeShield).Value,
                PdfModels.InfoElectrical.Create(request.Electrical.NominalVoltage, request.Electrical.NominalShield, request.Electrical.TypeGrounding).Value,
                PdfModels.InfoCable.Create(request.Cable.SupplyCable, request.Cable.CableOL).Value,
                request.DegreeProtection,
                PdfModels.InfoOmentum.Create(request.Omentum.TypeOmentum, request.Omentum.QuantityOmentum, request.Omentum.TypeOmentumOL, request.Omentum.QuantityOmentumOL).Value,
                request.PowerCable,
                request.Comment,
                PdfModels.InfoBuild.Create(request.Build.FullNameEngineer, request.Build.NumberOrderCustomer, request.Build.NumberBuild).Value
            );

            if (table1C.IsFailure)
            {
                return BadRequest(table1C.Error);
            }

            var tablePDF = new List<TablePDF>();

            foreach (var table in request.RenderingTableList)
            {
                var tablePDFs = TablePDF.Create(table.Name, table.NameOfScheme, table.Type,
                    table.NumberingLetter, table.NumberingDigit, table.Phase, table.Polus, table.Level, table.NumberBuild);

                if (tablePDFs.IsFailure)
                {
                    return BadRequest(tablePDFs.Error);
                }
                tablePDF.Add(tablePDFs.Value);
            }

            string outputPdfPath = Path.Combine(_filesPdfOutput, "output.pdf");

            await _historyPdfService.SaveHistoryAsync(tablePDF);

            // Действия на основе количества записей в tablePDF
            if (tablePDF.Count <= 7)
            {
                // Вертикальная развертка
                try
                {
                    await _generatorPdfService.GeneratePdfAsync(outputPdfPath, tablePDF.Count, tablePDF, table1C.Value);
                    return Ok(new { Message = "PDF generated successfully.", Path = outputPdfPath });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "Error generating PDF.", Details = ex.Message });
                }
            }
            else
            {
                try
                {
                    await _generatorPdfService.GeneratePdfMoreSevenAsync(outputPdfPath, tablePDF.Count, tablePDF, table1C.Value);
                    return Ok(new { Message = "PDF generated successfully.", Path = outputPdfPath });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { Message = "Error generating PDF.", Details = ex.Message });
                }

            }






        }
        

        [HttpGet("download")]
        public IActionResult DownloadPdf(string fileName)
        {
            var filePath = Path.Combine(_filesPdfOutput, fileName);

            if (!System.IO.File.Exists(filePath))
                return NotFound(new { Message = "File not found." });

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;

            return File(memory, "application/pdf", Path.GetFileName(filePath));
        }

        [HttpGet("api/GetSchemeDetails")]
        public async Task<IActionResult> GetSchemeDetails(string name)
        {
            try
            {
                // Получаем данные из метода
                var (type, letter, result, polus) = await _schemeDetailsRepositories.SchemeDetailsfAsync(name);

                // Формируем JSON-ответ
                return Ok(new
                {
                    type,
                    numberingLetter = letter,
                    nameOfScheme = result,
                    Polus = polus
                });
            }
            catch (Exception ex)
            {
                // Обрабатываем ошибку
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("api/GetNames")]
        public async Task<IActionResult> GetNames(string name)
        {
            try
            {
                // Получаем данные из метода
                string type = await _schemeDetailsRepositories.GetEquipmentTypeAsync(name);

                // Проверяем, является ли тип допустимым
                if (type != "Модульный автомат" && type != "Дифференциальный автомат" && type != "УЗО" && type !="Выключатель нагрузки")
                {
                    return NotFound(new { Message = "Оборудование не является модульным автоматом, дифференцированным автоматом или УЗО." });
                }

                // Формируем JSON-ответ
                return Ok(new
                {
                    name
                });
            }
            catch (Exception ex)
            {
                // Обрабатываем ошибку
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpGet("api/history")]
        public async Task<IActionResult> GetHistory()
        {
            var lastTenHistory = await _historyPdfService.GetLastTenHistoryRecordsAsync();
            return Ok(lastTenHistory.Select(kv => new { numberBuild = kv.Key, dateCreated = DateTime.Now }));
        }



        [HttpGet("api/history/{numberBuild}")]
        public async Task<IActionResult> GetBuild(string numberBuild)
        {
            var history = await _historyPdfService.LoadHistoryAsync();
            if (history.TryGetValue(numberBuild, out var build))
            {
                return Ok(new { numberBuild, tables = build });
            }
            return NotFound();
        }


    }
}
