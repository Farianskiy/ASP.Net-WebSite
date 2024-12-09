using CSharpFunctionalExtensions;

namespace WebSiteElectronicMind.Core.Models
{
    public class FileMetadata
    {
        public FileMetadata(string originalFileName, string uniqueFileName, string filePath, DateTime uploadTime)
        {
            OriginalFileName = originalFileName;
            UniqueFileName = uniqueFileName;
            FilePath = filePath;
            UploadTime = uploadTime;
        }

        public string OriginalFileName { get; private set; } = string.Empty;
        public string UniqueFileName { get; private set; } = string.Empty;
        public string FilePath { get; private set; } = string.Empty;
        public DateTime UploadTime { get; private set; }

        public static Result<FileMetadata> Create(string originalFileName, string uniqueFileName, string filePath)
        {
            if (string.IsNullOrEmpty(originalFileName))
            {
                return Result.Failure<FileMetadata>($"'{nameof(originalFileName)}' cannot be null or empty");
            }

            if (string.IsNullOrEmpty(uniqueFileName))
            {
                return Result.Failure<FileMetadata>($"'{nameof(uniqueFileName)}' cannot be null or empty");
            }

            if (string.IsNullOrEmpty(filePath))
            {
                return Result.Failure<FileMetadata>($"'{nameof(filePath)}' cannot be null or empty");
            }

            var FileMetadata = new FileMetadata(originalFileName, uniqueFileName, filePath, DateTime.Now);

            return Result.Success(FileMetadata);
        }
    }
}
