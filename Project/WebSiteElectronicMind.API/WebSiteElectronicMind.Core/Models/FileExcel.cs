using CSharpFunctionalExtensions;

namespace WebSiteElectronicMind.Core.Models
{
    public class FileExcel
    {
        public FileExcel(string fileName, string fileCode, string filePath)
        {
            FileName = fileName;
            FileCode = fileCode;
            FilePath = filePath;
        }
        public string FileCode { get; private set; } = string.Empty;

        public string FileName { get; private set; } = string.Empty;

        public string FilePath { get; private set; } = string.Empty;

        public static Result<FileExcel> Create(string fileName, string fileCode, string filePath)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return Result.Failure<FileExcel>($"'{nameof(fileName)}' cannot be null or empty");
            }

            if (string.IsNullOrEmpty(filePath))
            {
                return Result.Failure<FileExcel>($"'{nameof(filePath)}' cannot be null or empty");
            }

            var FileExcel = new FileExcel(fileName, fileCode, filePath);

            return Result.Success(FileExcel);
        }
    }
}