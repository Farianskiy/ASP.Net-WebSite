using Microsoft.AspNetCore.Mvc;
using WebSiteElectronicMind.API.Contracts;
using WebSiteElectronicMind.Application.Services;
using WebSiteElectronicMind.Core.Models;

namespace WebSiteElectronicMind.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PositionController : ControllerBase
    {
        private readonly string _filesExcelInput =
            Path.Combine(Directory.GetCurrentDirectory(), "Files/Excel/Input");
        private readonly string _filesExcelOutput =
            Path.Combine(Directory.GetCurrentDirectory(), "Files/Excel/Output");

        private readonly FileExcelService _fileExcelService;
        private readonly IReadDataFromExcelService _readDataFromExcelService;
        private readonly IPositionService _positionService;
        private readonly IWriteDataToExcelService _writeDataToExcelService;
        private readonly FileMetadataService _fileMetadataService;

        public PositionController(FileExcelService fileExcelService, IReadDataFromExcelService readDataFromExcelService, IPositionService positionService, IWriteDataToExcelService writeDataToExcelService, FileMetadataService fileMetadataService)
        {
            _fileExcelService = fileExcelService;
            _readDataFromExcelService = readDataFromExcelService;
            _positionService = positionService;
            _writeDataToExcelService = writeDataToExcelService;
            _fileMetadataService = fileMetadataService;
        }

        [HttpPost]
        public async Task<ActionResult> CreatePosition([FromForm]PositionRequest request)
        {
            // Сохранение Excel файла
            var fileExcelResult = await _fileExcelService.CreateFileExcel(request.FileExcel, _filesExcelInput);

            if (fileExcelResult.IsFailure)
            {
                return BadRequest(fileExcelResult.Error);
            }

            // Чтение данных из Excel файла
            var readResult = await _readDataFromExcelService.ReadExcelML(fileExcelResult.Value.FilePath);

            if (readResult.IsFailure)
            {
                return BadRequest(readResult.Error);
            }

            var Positions = new List<Position>();
            var errors = new List<string>();

            foreach (var position in readResult.Value)
            {

                var updateResult = await _positionService.UpdateTypeOfCharacteristic(position);

                if (updateResult.IsFailure)
                {
                    errors.Add(updateResult.Error);
                }
                else
                {
                    Positions.Add(updateResult.Value);
                }
            }


            if (errors.Any())
            {
                return BadRequest(string.Join(", ", errors));
            }

            foreach (var position in Positions)
            {
                Console.WriteLine($"Position: {position}");
            }

            // Запись данных в Excel
            var outputFilePath = Path.Combine(_filesExcelOutput, request.TitelFileName + ".xlsx");
            await _writeDataToExcelService.WriteExcelML(Positions, outputFilePath);

            // Вызов метода сервиса для создания и сохранения метаданных файла
            var saveMetadataResult = _fileMetadataService.CreateAndSaveMetadata(request.FileExcel.FileName, Path.GetFileName(outputFilePath), outputFilePath);
            if (saveMetadataResult.IsFailure)
            {
                return BadRequest(saveMetadataResult.Error);
            }

            return Ok();


        }


        [HttpGet("latest-files")]
        public ActionResult<IEnumerable<FileMetadata>> GetLatestFiles()
        {
            var metadataList = _fileMetadataService.LoadMetadataList();
            var latestFiles = metadataList.OrderByDescending(m => m.UploadTime).Take(6).ToList();
            return Ok(latestFiles);
        }

        [HttpGet("download-files")]
        public IActionResult GetFile([FromQuery] string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var fileName = Path.GetFileName(filePath);
            return File(fileBytes, "application/octet-stream", fileName);
        }


    }
}
