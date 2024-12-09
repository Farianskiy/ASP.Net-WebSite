using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using WebSiteElectronicMind.Core.Models;

namespace WebSiteElectronicMind.Application.Services
{
    public class FileExcelService
    {
        private readonly string _metadataPath = "metadata.json";

        public async Task<Result<FileExcel>> CreateFileExcel(IFormFile fileExcel, string path)
        {
            try
            {
                var fileCode = Guid.NewGuid().ToString();
                var fileExtension = Path.GetExtension(fileExcel.FileName);
                var fileName = $"{fileCode}{fileExtension}";
                var filePath = Path.Combine(path, fileName);

                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await fileExcel.CopyToAsync(stream);
                }

                var file = FileExcel.Create(filePath, fileCode, filePath);

                return file;
            }
            catch(Exception ex)
            {
                return Result.Failure<FileExcel>(ex.Message);
            }
        }
    }
}